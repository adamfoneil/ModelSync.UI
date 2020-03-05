using JsonSettings.Library;
using System.Collections.Generic;
using WinForms.Library.Models;
using static System.Environment;

namespace ModelSync.App
{
    public class Settings : SettingsBase
    {
        public FormPosition Position { get; set; }
        public string SolutionFolder { get; set; } = BuildPath(SpecialFolder.MyDocuments);

        /// <summary>
        /// cached by Open Solution dialog under the SolutionFolder set above
        /// </summary>
        public List<string> SolutionFiles { get; set; }

        public override string Filename => BuildPath(SpecialFolder.LocalApplicationData, "ModelSync", "_Settings.json");
    }
}
