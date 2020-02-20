using JsonSettings.Library;
using WinForms.Library.Models;

namespace ModelSync.WinForms
{
    public class Settings : SettingsBase
    {
        public FormPosition Position { get; set; }

        public override string Filename => throw new System.NotImplementedException();
    }
}
