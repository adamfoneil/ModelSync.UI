﻿using ModelSync.App.Helpers;
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
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += AssemblyHelper.LoadDependencies;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain() { StartupArgs = args });
        }
    }
}
