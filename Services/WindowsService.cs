using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WebcamController.Services
{
    public class WindowsService
    {
        public void SetStartup(bool enable)
        {
            const string registryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            string appName = Application.ProductName;

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, true))
            {
                if (enable)
                {
                    key.SetValue(appName, $"\"{Application.ExecutablePath}\" /minimized");
                }
                else if (key.GetValue(appName) != null)
                {
                    key.DeleteValue(appName, false);
                }
            }
        }

        public bool IsStartupEnabled()
        {
            const string registryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            string appName = Application.ProductName;
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, false))
            {
                return key?.GetValue(appName) != null;
            }
        }

        public void OpenProgramFolder()
        {
            Process.Start("explorer.exe", Application.StartupPath);
        }
    }
}
