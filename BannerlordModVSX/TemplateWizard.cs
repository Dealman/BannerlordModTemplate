using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;
using EnvDTE;
using WizardInterfaceWPF;
using System.Windows;
using System.IO;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using NuGet.VisualStudio;
using System.Xml.Linq;
using System.Linq;

namespace BannerlordModVSX
{
    public class TemplateWizard : IWizard
    {
        // TODO: This can probably be cleaned up a bit
        private WizardWindow wizardWindow;
        private string bannerlordDirectory;
        private string bannerlordExe;
        private bool createSubModule;
        private bool createReadme;
        private bool addHarmony;
        private bool useLauncherMods;
        private Dictionary<string, string> _replacementsDictionary;
        private List<string> _nugetPackages = new List<string>();

        public void BeforeOpeningFile(ProjectItem projectItem)
        {

        }

        public void ProjectFinishedGenerating(Project project)
        {
            // Visual Studio wants this or it gets very sad
            ThreadHelper.ThrowIfNotOnUIThread();
            // Get the newly created project's root directory, so we can easily add files to it if we want
            string projectRoot = Path.Combine(project.FullName, @"..\");

            #region launchSettings.json Creation
            // This is nasty, but wasn't sure how to otherwise add launchSettings.json via vstemplate. TODO: Refactor, Regex?
            JRaw rawJSON = new JRaw("{ \"profiles\": { \"Default Debugging Template\": { \"commandName\": \"Executable\", \"executablePath\": \""+_replacementsDictionary["$BannerlordExecutable$"].Replace(@"\", @"\\").Replace("&amp;", "&") +"\", \"commandLineArgs\": \"" + _replacementsDictionary["$BannerlordDebugArgs$"] + "\", \"workingDirectory\": \""+_replacementsDictionary["$BannerlordDirectory$"].Replace(@"\", @"\\").Replace("&amp;", "&") + @"\\Modules\\" +_replacementsDictionary["$safeprojectname$"] + @"\\bin\\Win64_Shipping_Client""}}}");
            using(StreamWriter configFile = File.CreateText(projectRoot + @"\Properties\launchSettings.json"))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(configFile, rawJSON);
            }
            #endregion

            #region Installation of NuGet Packages
            // Install nuget packages | TODO: Needs to be updated if more packages are added, map each package to a bool or something
            // but we only have Harmony at the moment, so it can wait for now
            if(addHarmony)
            {
                var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
                var installer = componentModel.GetService<IVsPackageInstaller2>();

                foreach (var package in _nugetPackages)
                {
                    installer.InstallLatestPackage(null, project, package, false, false);
                }
            }
            #endregion

            #region Specific Item Removal (SubModule, Readme...)
            ProjectItems projectItems = project.ProjectItems;
            DTE _dte = project.DTE;
            // TODO: Fix? This WAS working just fine, now it's stopped working for no apparent reason...
            // Debug check DTE, Solution, FindProjectItem etc etc

            if (!createSubModule)
                _dte.Solution.FindProjectItem("SubModule.cs").Remove();

            if (!createReadme)
                _dte.Solution.FindProjectItem("Readme.txt").Remove();
            #endregion

            //string csProjFile = projectRoot + $"{_replacementsDictionary["$safeprojectname$"]}.csproj";
            //Debug.WriteLine($"[DEBUG]: {project.FullName}");
            //System.Threading.Thread.Sleep(6000);
            //string fileContents = File.ReadAllText(project.FullName);
            Configuration tet = project.ConfigurationManager.ActiveConfiguration;
            //Debug.WriteLine($"[DEBUG]: Contains Stuff: {fileContents.Contains("<Compile Include=\"SubModule.cs\" />")}");
            //File.WriteAllText(project.FullName, fileContents.Replace("<Compile Include=\"SubModule.cs\" />", ""));
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            // Executed after an ItemTemplate is finished generating
        }

        public void RunFinished()
        {
            // Executed after the project is finished generating
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            // Store a copy of the replacements dictionary, used later for deleting the project if it was cancelled
            _replacementsDictionary = replacementsDictionary;
            string projectName = replacementsDictionary["$projectname$"];
            string destinationDirectory = replacementsDictionary["$destinationdirectory$"];

            #region VSTemplate Parsing for NuGet Packages
            // Parse the VSTemplate for nuget package entries, this is the only way AFAIK to make them optional but at the detriment of them having to be downloaded
            // instead of locally available in the VSIX...
            if (customParams.Length > 0)
            {
                var vsTemplate = XDocument.Load((string)customParams[0]);
                IEnumerable<XElement> parsedPackages = vsTemplate.Descendants().Where(x => x.Name.LocalName == "package");

                foreach(XElement parsedPackage in parsedPackages)
                {
                    _nugetPackages.Add(parsedPackage.Attribute("id").Value);
                }
            }
            #endregion

            try
            {
                // Create a new instance of the WizardWindow
                wizardWindow = new WizardWindow();

                if(wizardWindow.ShowDialog().Value == true)
                {
                    // Set up the paths we need for the configuration. & should automatically be escaped with &amp;
                    bannerlordDirectory = wizardWindow.BannerlordDirectory;
                    bannerlordExe = bannerlordDirectory + @"\bin\Win64_Shipping_Client\Bannerlord.exe";
                    createSubModule = wizardWindow.IncludeSubModule;
                    createReadme = wizardWindow.IncludeReadme;
                    addHarmony = wizardWindow.IncludeHarmony;
                    useLauncherMods = wizardWindow.UseLauncherMods;

                    // Parse the Bannerlord Launcher to find what modules were last used
                    StringBuilder argumentString = new StringBuilder();
                    List<string> moduleList = wizardWindow.LauncherMods;
                    
                    if(moduleList != null && moduleList.Count >= 1 && useLauncherMods)
                    {
                        argumentString.Append("/singleplayer _MODULES_*");
                        foreach(string module in moduleList)
                        {
                            argumentString.Append($"{module}*");
                        }
                        argumentString.Append($"{replacementsDictionary["$safeprojectname$"]}*_MODULES_");
                    } else {
                        argumentString.Append($"/singleplayer _MODULES_*Native*SandBoxCore*SandBox*StoryMode*CustomBattle*{replacementsDictionary["$safeprojectname$"]}*_MODULES_");
                    }

                    // Add our custom replacements to the dictionary
                    replacementsDictionary.Add("$BannerlordDirectory$", bannerlordDirectory);
                    replacementsDictionary.Add("$BannerlordExecutable$", bannerlordExe);
                    replacementsDictionary.Add("$BannerlordDebugArgs$", argumentString.ToString());

                    // We should be done now, close the window
                    wizardWindow.Close();
                } else {
                    // The user clicked cancel or closed the window, throw the exception
                    throw new WizardBackoutException();
                }
            } catch (Exception ex) {
                // Project folder would still have been created, clean it up if the user decided to back out
                string projectFolder = Path.GetFullPath(Path.Combine(destinationDirectory, @"..\"));

                if(Directory.Exists(projectFolder))
                {
                    Directory.Delete(projectFolder, true);
                } else {
                    Directory.Delete(destinationDirectory, true);
                }
                throw;
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
