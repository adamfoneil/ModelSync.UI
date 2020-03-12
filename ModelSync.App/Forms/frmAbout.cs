using System;
using System.Windows.Forms;

namespace ModelSync.App.Forms
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text = Application.ProductVersion;
        }
    }
}
