using JsonSettings.Library;
using System;
using System.Collections.Generic;

namespace ModelSync.App.Models
{
    internal class SolutionOpenHistory : SettingsBase
    {
        public SolutionOpenHistory()
        {
            History = new Dictionary<string, DateTime>();
        }

        public Dictionary<string, DateTime> History { get; set; }        

        public override string Filename => BuildPath(Environment.SpecialFolder.LocalApplicationData, "ModelSync", "_SolutionHistory.json");
    }
}
