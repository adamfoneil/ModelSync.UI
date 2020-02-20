using JsonSettings.Library;
using WinForms.Library.Models;
using static System.Environment;

namespace ModelSync.App
{
    public class Settings : SettingsBase
    {
        public FormPosition Position { get; set; }

        public override string Filename => BuildPath(SpecialFolder.LocalApplicationData, "ModelSync", "Settings.json");
    }
}
