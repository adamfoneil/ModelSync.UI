using JsonSettings.Library;
using System;
using System.Windows.Forms;
using WinForms.Library.Models;
using WinForms.Library;
using ModelSync.App.Controls;

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

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            int lastIndex = tabMain.TabPages.Count - 1;
            if (tabMain.SelectedIndex == lastIndex)
            {
                var tab = new TabPage($"profile {tabMain.TabPages.Count}");
                var ui = new SyncUI() { Dock = DockStyle.Fill };
                tab.Controls.Add(ui);
                tabMain.TabPages.Insert(lastIndex, tab);
                tabMain.SelectedIndex = lastIndex;
            }
        }
    }
}
