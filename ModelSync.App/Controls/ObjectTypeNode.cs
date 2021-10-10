using ModelSync.Abstract;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ModelSync.App.Controls
{
    internal class ObjectTypeNode : TreeNode
    {
        public ObjectTypeNode(ObjectType objectType, int count)
        {
            Text = $"{objectType} ({count})";
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
            { ObjectType.ForeignKey, "shortcut" },
            { ObjectType.CheckConstraint, "check" },
            { ObjectType.Procedure, "proc" },
            { ObjectType.View, "view" },
            { ObjectType.TableFunction, "table-func" },
            { ObjectType.TableType, "table" }
        };
    }
}
