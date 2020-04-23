using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace WizardInterfaceWPF
{
    public static class GameFinder
    {
        public static string GetLocationViaUninstallEntry()
        {
            try
            {
                RegistryKey baseRegKey64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey subRegKey64 = baseRegKey64.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 261550", System.Security.AccessControl.RegistryRights.ReadKey);

                if (subRegKey64 != null)
                {
                    string installLocation = subRegKey64.GetValue("InstallLocation", null).ToString();

                    if(!string.IsNullOrWhiteSpace(installLocation))
                    {
                        return installLocation;
                    }
                }

                return null;
            } catch (Exception ex) {
                MessageBox.Show($"Warning!\n\nAn error occurred whilst trying to find the install location of M&B 2: Bannerlord!\nError Message:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return null;
            }
        }

        public static bool VerifyInstallPath(string path)
        {
            if(!string.IsNullOrWhiteSpace(path))
            {
                if (File.Exists(path + @"\bin\Win64_Shipping_Client\Bannerlord.exe"))
                {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }       
        }

        public static List<string> GetLauncherModules()
        {
            List<string> moduleList = new List<string>();
            string documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string launcherConfig = documentsDirectory + @"\Mount and Blade II Bannerlord\Configs\LauncherData.xml";

            if(File.Exists(launcherConfig))
            {
                XDocument config = XDocument.Load(launcherConfig);
                if(config != null)
                {
                    var xmlQuery = from x in config.Root.Descendants("UserModData")
                                   where ((bool)x.Element("IsSelected") == true && (x.Parent.Parent.Name != "MultiplayerData")) // Prevent it from getting MP modules
                                   select x.Element("Id");

                    if(xmlQuery != null)
                    {
                        foreach(string moduleName in xmlQuery)
                        {
                            moduleList.Add(moduleName);
                        }
                        return moduleList;
                    }
                }
                return null;
            } else {
                return null;
            }
        }
    }
}
