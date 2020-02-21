using JsonSettings;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;

namespace ModelSync.App.Models
{
    public enum SourceType
    {
        Assembly,
        Connection
    }

    public class MergeDefinition
    {
        public string Title { get; set; } = "merge";

        public SourceType SourceType { get; set; }

        [JsonProtect(DataProtectionScope.CurrentUser)]
        public string Source { get; set; }

        [JsonProtect(DataProtectionScope.CurrentUser)]
        public string Destination { get; set; }

        public Color BackgroundColor { get; set; }

        public List<string> IgnoreObjects { get; set; }
    }
}
