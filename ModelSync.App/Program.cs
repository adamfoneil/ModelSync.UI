using ModelSync.App.Helpers;
using ModelSync.App.Services;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace ModelSync.App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!StartLicensing()) return;

            AppDomain.CurrentDomain.AssemblyResolve += delegate (object sender, ResolveEventArgs resolveArgs)
            {
                if (AssemblyHelper.FindNugetPackageFromAssemblyName(resolveArgs.Name, out string fileName))
                {
                    return Assembly.LoadFile(fileName);                    
                }

                return null;
            };

            Application.Run(new frmMain() { StartupArgs = args });
        }

        private static bool StartLicensing()
        {
            return new AppGatekeeper().StartLicensing();
        }
    }
}
