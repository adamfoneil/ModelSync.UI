using System.Windows.Forms;

namespace ModelSync.App.Controls
{
    public class RepoNode : TreeNode
    {
        public RepoNode(string text) : base(text)
        {
            ImageKey = "repo";
            SelectedImageKey = "repo";
        }
    }
}
