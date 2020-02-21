using System.Collections.Generic;
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
    }
}
