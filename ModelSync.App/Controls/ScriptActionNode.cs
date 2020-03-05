using ModelSync.Library.Models;
using System.Windows.Forms;

namespace ModelSync.App.Controls
{
    internal class ScriptActionNode : TreeNode
    {
        public ScriptActionNode(ScriptAction scriptAction)
        {
            Text = scriptAction.Object.ToString();
            ScriptAction = scriptAction;
            ImageKey = ObjectTypeNode.ObjectTypeIcons[scriptAction.Object.ObjectType];
            SelectedImageKey = ImageKey;
        }

        public ScriptAction ScriptAction { get; }
    }
}
