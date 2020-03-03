using System.Windows.Forms;

namespace ModelSync.App.Controls
{
    public class MergeDefinitionTab : TabPage
    {        
        public MergeDefinitionTab(string solutionFile, string text) : base(text)
        {
            SolutionFile = solutionFile;
        }

        public string SolutionFile { get; }
    }
}
