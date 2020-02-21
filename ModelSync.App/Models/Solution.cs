using System.Collections.Generic;

namespace ModelSync.App.Models
{
    public class Solution
    {
        public List<MergeDefinition> Merges { get; set; }

        /// <summary>
        /// original .sln file this is related to
        /// </summary>
        public string VisualStudioSolution { get; set; }
    }
}
