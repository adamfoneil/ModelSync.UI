using ModelSync.Library.Abstract;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ModelSync.App.Controls
{
    internal class ObjectTypeNode : TreeNode
    {
        public ObjectTypeNode(ObjectType objectType, int count)
        {
            Text = $"{objectType.ToString()} ({count})";
            ObjectType = objectType;
            ImageKey = ObjectTypeIcons[objectType];
            SelectedImageKey = ImageKey;
        }

        public ObjectType ObjectType { get; }

        public static Dictionary<ObjectType, string> ObjectTypeIcons = new Dictionary<ObjectType, string>()
        {
            { ObjectType.Schema, "schema" },
            { ObjectType.Table, "table" },
            { ObjectType.Column, "column" },
            { ObjectType.Index, "key" },
            { ObjectType.ForeignKey, "shortcut" }
        };
    }
}
