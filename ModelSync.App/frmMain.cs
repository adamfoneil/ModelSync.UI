using JsonSettings.Library;
using System;
using System.Windows.Forms;
using WinForms.Library.Models;
using WinForms.Library;

namespace ModelSync.App
{
    public partial class frmMain : Form
    {
        private Settings _settings;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                _settings = SettingsBase.Load<Settings>();
                _settings.Position?.Apply(this);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _settings.Position = FormPosition.FromForm(this);
                _settings.Save();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
