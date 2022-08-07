using ModelSync.App.Models;
using ModelSync.App.Services;
using System;
using System.Windows.Forms;

namespace ModelSync.App.Forms
{
    public partial class frmAbout : Form
    {
        AppInstallHelper _installer = new AppInstallHelper();

        public frmAbout()
        {
            InitializeComponent();
        }

        private async void frmAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text = Application.ProductVersion;

            llSaveFolder.Url = Solution.LocalSaveFolder;
            llSaveFolder.Text = Solution.LocalSaveFolder;

            var check = await _installer.GetVersionCheckAsync();
            panel1.Visible = check.IsNew;
            lblNewVersion.Text = $"Version {check.Version} available:";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _installer.AutoInstallAsync();
        }
    }
}
