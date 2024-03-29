﻿using JsonSettings;
using ModelSync.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;

namespace ModelSync.App.Models
{
    public enum SourceType
    {
        Assembly,
        Connection,
        JsonFile
    }

    public class MergeDefinition
    {
        public string Title { get; set; } = "merge";

        public SourceType SourceType { get; set; }

        [JsonProtect(DataProtectionScope.CurrentUser)]
        public string Source { get; set; }

        [JsonProtect(DataProtectionScope.CurrentUser)]
        public string Destination { get; set; }

        public Color BackgroundColor { get; set; } = Color.Transparent;

        public List<ExcludeAction> ExcludeActions { get; set; }
    }
}
