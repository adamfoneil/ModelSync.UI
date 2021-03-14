using ModelSync.App.Helpers;
using ModelSync.Models;
using System.Windows.Forms;

namespace ModelSync.App.Controls
{
    internal enum ExcludeActionScope
    {
        Local,
        Shared
    }

    internal class ExcludeActionNode : TreeNode
    {
        public ExcludeActionNode(ExcludeAction excludeAction)
        {
            Text = excludeAction.ObjectName;
            ExcludeAction = excludeAction;
            ImageKey = ObjectTypeNode.ObjectTypeIcons[excludeAction.ObjectType];
            SelectedImageKey = ImageKey;
        }

        public ExcludeAction ExcludeAction { get; }

        public ExcludeActionScope Scope 
        {
            get => this.IsChildOf(node => node.ImageKey.Equals("repo")) ? 
                ExcludeActionScope.Shared : 
                ExcludeActionScope.Local; 
        }                
    }
}
