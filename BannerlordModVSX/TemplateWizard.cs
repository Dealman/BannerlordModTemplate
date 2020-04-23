using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;
using EnvDTE;
using WizardInterfaceWPF;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using NuGet.VisualStudio;
using System.Xml.Linq;
using System.Linq;
using System.Windows;

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

            if (!createSubModule)
                _dte.Solution.FindProjectItem("SubModule.cs").Remove();

            if (!createReadme)
                _dte.Solution.FindProjectItem("Readme.txt").Remove();
            #endregion
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

                if(ex.InnerException is WizardBackoutException || ex.InnerException is WizardCancelledException)
                {
                    string projectFolder = Path.GetFullPath(Path.Combine(destinationDirectory, @"..\"));

                    if (Directory.Exists(projectFolder))
                    {
                        Directory.Delete(projectFolder, true);
                    } else {
                        Directory.Delete(destinationDirectory, true);
                    }
                } else {
                    MessageBox.Show($"An error has occurred!\n\nError Message:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
