using System.Windows.Forms;

namespace ModelSync.App.Forms
{
    public partial class frmSaveTestCase : Form
    {
        public frmSaveTestCase()
        {
            InitializeComponent();
        }

        public bool IsCorrect
        {
            get { return rbCorrect.Checked; }
        }

        public string Comments
        {
            get { return tbIncorrectNotes.Text; }
        }
    }
}
