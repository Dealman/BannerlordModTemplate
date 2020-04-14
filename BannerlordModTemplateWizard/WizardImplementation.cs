using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TemplateWizard;
using System.Windows.Forms;
using EnvDTE;
using System.Text;

namespace BannerlordModTemplateWizard
{
    public class WizardImplementation : IWizard
    {
        private WizardForm myForm;
        private string bannerlordPath;
        private string bannerlordExe;
        private string bannerlordAssemblies;
        private string bannerlordModules;
        private bool createSubModule;
        private bool createReadme;

        // This method is called before opening any item that has the OpenInEditor attribute.
        public void BeforeOpeningFile(ProjectItem projectItem)
        {

        }
        public void ProjectFinishedGenerating(Project project)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            ProjectItems projectItems = project.ProjectItems;
            ProjectItem subModuleItem = (ProjectItem)null;
            ProjectItem readmeItem = (ProjectItem)null;

            foreach (ProjectItem projectItem in projectItems)
            {
                if (projectItem.Name.Contains("SubModule.cs") && subModuleItem == null)
                    subModuleItem = projectItem;

                if (projectItem.Name.Contains("Readme.txt") && readmeItem == null)
                    readmeItem = projectItem;
            }

            if (subModuleItem != null && !createSubModule)
                subModuleItem.Remove();

            if (readmeItem != null && !createReadme)
                readmeItem.Remove();
        }
        // This method is only called for item templates, not for project templates.
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {

        }
        // This method is called after the project is created.
        public void RunFinished()
        {

        }
        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            try
            {
                myForm = new WizardForm();
                DialogResult result = myForm.ShowDialog();

                if (result == DialogResult.Yes)
                {
                    bannerlordPath = myForm.BannerlordPath;
                    bannerlordExe = bannerlordPath + @"\bin\Win64_Shipping_Client\Bannerlord.exe";
                    bannerlordAssemblies = bannerlordPath + @"\bin\Win64_Shipping_Client";
                    bannerlordModules = bannerlordPath + @"\Modules";

                    createSubModule = myForm.IncludeSubModule;
                    createReadme = myForm.IncludeReadme;

                    StringBuilder argumentString = new StringBuilder();
                    Console.WriteLine($"[DEBUG]: Module List Count {myForm.ModuleList.Count}");
                    if (myForm.ModuleList.Count >= 1 && myForm.UseLauncherMods)
                    {
                        argumentString.Append("/singleplayer _MODULES_*");

                        foreach (string moduleName in myForm.ModuleList)
                        {
                            argumentString.Append($"{moduleName}*");
                        }
                    } else {
                        argumentString.Append("/singleplayer _MODULES_*Native*SandBoxCore*SandBox*StoryMode*");
                    }

                    //replacementsDictionary.Add("$bannerlordpath$", bannerlordPath);
                    replacementsDictionary.Add("$bannerlordexe$", bannerlordExe);
                    replacementsDictionary.Add("$bannerlordassemblies$", bannerlordAssemblies);
                    replacementsDictionary.Add("$bannerlordmodules$", bannerlordModules);
                    replacementsDictionary.Add("$bannerlordargsprefix$", argumentString.ToString());
                } else {

                }
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
        // This method is only called for item templates, not for project templates.
        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
