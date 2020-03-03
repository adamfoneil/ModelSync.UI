using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModelSync.App.Models
{
    public class Solution
    {
        public List<MergeDefinition> Merges { get; set; }

        public static Solution Create()
        {
            return new Solution()
            {
                Merges = new MergeDefinition[] { new MergeDefinition() }.ToList()
            };
        }

        public static string GetFilename(string visualStudioSolution)
        {
            string baseFile = Path.GetFileNameWithoutExtension(visualStudioSolution);
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ModelSync", baseFile + ".json");
        }
    }
}
