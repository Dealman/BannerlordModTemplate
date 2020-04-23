using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace WizardInterfaceWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WizardWindow : MetroWindow
    {
        // TODO: Two strings for directory? One escaped, one non-escaped?
        public string BannerlordDirectory {get{ return PathTextBox.Text.Replace("&", "&amp;"); }}
        public bool IncludeSubModule {get{ return IncludeSubModuleCheckBox.IsChecked.Value; }}
        public bool IncludeReadme {get{ return IncludeReadmeCheckBox.IsChecked.Value; }}
        public bool IncludeHarmony {get{ return IncludeHarmonyCheckBox.IsChecked.Value; }}
        public bool UseLauncherMods {get{ return UseLauncherModulesCheckBox.IsChecked.Value; }}
        public List<string> LauncherMods {get{ return GameFinder.GetLauncherModules(); }}


        public WizardWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick_Manager(object sender, RoutedEventArgs e)
        {
            if(sender == BrowsePathButton)
            {
                using (var folderBrowser = new WinForms.FolderBrowserDialog())
                {
                    folderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;
                    folderBrowser.ShowNewFolderButton = false;
                    folderBrowser.Description = "Browse to M&B 2: Bannerlord Root Folder";

                    WinForms.DialogResult dialogResult = folderBrowser.ShowDialog();
                    if(dialogResult == WinForms.DialogResult.OK)
                    {
                        if(GameFinder.VerifyInstallPath(folderBrowser.SelectedPath))
                        {
                            ConfirmButton.IsEnabled = true;
                            PathTextBox.Text = folderBrowser.SelectedPath;
                        } else {
                            BannerlordPathFailed(1);
                        }
                    } else {
                        // User cancelled or closed the dialog
                    }
                }
            }

            if (sender == ConfirmButton)
                this.DialogResult = true;

            if (sender == CancelButton)
                this.DialogResult = false;

            if (sender == GitHubButton)
                System.Diagnostics.Process.Start("https://github.com/Dealman/BannerlordModTemplate");

            if (sender == ForumButton)
                System.Diagnostics.Process.Start("https://forums.taleworlds.com/index.php?threads/release-mod-template-for-visual-studio-automatically-configs-adds-references-and-more.413981/");
        }

        private void MetroWindow_ContentRendered(object sender, EventArgs e)
        {
            string installPath = GameFinder.GetLocationViaUninstallEntry();
            if(GameFinder.VerifyInstallPath(installPath))
            {
                ConfirmButton.IsEnabled = true;
                PathTextBox.Text = installPath;
            } else {
                BannerlordPathFailed(0);
            }
        }

        private async void BannerlordPathFailed(int reason)
        {
            switch(reason)
            {
                case 0:
                    await this.ShowMessageAsync("Warning", "Unable to automatically locate M&B 2: Bannerlord.\n\nPlease, try and locate it manually instead.", MessageDialogStyle.Affirmative);
                    break;
                case 1:
                    await this.ShowMessageAsync("Warning", "Selected folder failed to verify! Are you sure this is the root folder for Bannerlord? Try again.\n\nExample:\n"+ @"C:\Program Files (x86)\Steam\steamapps\common\Mount & Blade II Bannerlord", MessageDialogStyle.Affirmative);
                    break;
            }
        }
    }
}
