using ModelSync.Library.Models;
using System.Windows.Forms;

namespace ModelSync.App.Controls
{
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
    }
}
