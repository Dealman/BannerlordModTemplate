using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace BannerlordModTemplateWizard
{
    public partial class WizardForm : Form
    {
        private string bannerlordPath;
        public string BannerlordPath {
            get { return bannerlordPath; }
            set { bannerlordPath = value; }
        }
        public bool IncludeSubModule {
            get { return cbIncludeSubModule.Checked; }
        }
        public bool IncludeReadme {
            get { return cbIncludeReadme.Checked; }
        }

        #region Forms Methods/Events
        public WizardForm()
        {
            InitializeComponent();
        }

        private void WizardForm_Shown(object sender, EventArgs e)
        {
            GetBannerlordInstallLocation();
        }

        private void GetBannerlordInstallLocation()
        {
            try
            {
                RegistryKey baseRegKey64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey subRegKey64 = baseRegKey64.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 261550", System.Security.AccessControl.RegistryRights.ReadKey);

                if(subRegKey64 != null)
                {
                    string installLocation = subRegKey64.GetValue("InstallLocation", null).ToString();
                    if(!string.IsNullOrWhiteSpace(installLocation))
                    {
                        BannerlordPath = installLocation.Replace("&", "&amp;");
                        tbBannerlordPath.Text = installLocation;
                        bConfirm.Enabled = true;
                    } else {
                        MessageBox.Show($"Unable to automatically locate the path to Mount & Blade 2: Bannerlord. Please try specifying the path manually.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                } else {
                    MessageBox.Show($"Unable to automatically locate the path to Mount & Blade 2: Bannerlord. Please try specifying the path manually.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } catch (Exception ex) {
                MessageBox.Show($"An error has occurred!\n\nError Message:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bBrowsePath_Click(object sender, EventArgs e)
        {
            using(var fbd = new FolderBrowserDialog())
            {
                fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                fbd.Description = "Browse to the Bannerlord Installation Folder";
                DialogResult result = fbd.ShowDialog();

                if(result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    if(File.Exists($"{fbd.SelectedPath}\\bin\\Win64_Shipping_Client\\Bannerlord.exe"))
                    {
                        BannerlordPath = fbd.SelectedPath;
                        tbBannerlordPath.Text = fbd.SelectedPath;
                        bConfirm.Enabled = true;
                    } else {
                        MessageBox.Show($"Unable to locate bin\\Win64_Shipping_Client\\Bannerlord.exe, are you sure this is the root folder of Bannerlord?\n\nPlease verify the path and try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        bConfirm.Enabled = false;
                    }
                }
            }
        }
        #endregion

        private void bConfirm_Click(object sender, EventArgs e)
        {
            //Close();
        }
    }
}
