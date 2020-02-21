using System;
using System.Windows.Forms;

namespace ModelSync.App.Forms
{
    public partial class frmRename : Form
    {
        public frmRename()
        {
            InitializeComponent();
        }

        public string RenameText
        {
            get { return tbText.Text; }
            set { tbText.Text = value; }
        }

        private void tbText_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = !string.IsNullOrEmpty(tbText.Text);
        }
    }
}
