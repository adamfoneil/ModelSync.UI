using System;
using System.Windows.Forms;

namespace ModelSync.App.Helpers
{
    internal static class TreeViewHelpers
    {
        internal static bool IsChildOf(this TreeNode currentNode, Func<TreeNode, bool> filter)
        {
            var parent = currentNode.Parent;

            do
            {                
                if (parent == null) return false;
                if (filter.Invoke(parent)) return true;
                parent = parent.Parent;
            } while (true);                       
        }
    }
}
