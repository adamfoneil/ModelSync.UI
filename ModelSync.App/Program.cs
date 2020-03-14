using ModelSync.App.Services;
using System;
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

            Application.Run(new frmMain() { StartupArgs = args });
        }

        private static bool StartLicensing()
        {
            return new AppGatekeeper().StartLicensing();
        }
    }
}
