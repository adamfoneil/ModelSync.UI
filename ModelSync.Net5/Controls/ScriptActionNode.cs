using ModelSync.Models;
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
            ActionRequired = ((scriptAction.Object as Column)?.DefaultValueRequired ?? false);
        }

        public ScriptAction ScriptAction { get; }
        public bool ActionRequired { get; set; }
    }
}
