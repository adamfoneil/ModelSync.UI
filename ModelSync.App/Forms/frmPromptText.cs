using System;
using System.Windows.Forms;

namespace ModelSync.App.Forms
{
    public partial class frmPromptText : Form
    {
        public frmPromptText()
        {
            InitializeComponent();
        }        

        public string NewText
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
