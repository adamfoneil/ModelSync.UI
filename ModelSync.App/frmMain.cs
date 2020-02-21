using JsonSettings;
using JsonSettings.Library;
using ModelSync.App.Controls;
using ModelSync.App.Models;
using System;
using System.IO;
using System.Windows.Forms;
using WinForms.Library.Models;

namespace ModelSync.App
{
    public partial class frmMain : Form
    {
        private Settings _settings;
        private Solution _solution;
        private string _solutionFile;

        public string[] StartupArgs { get; set; }       

        public frmMain()
        {
            InitializeComponent();
        }
        
        public string SolutionFile
        {
            get { return _solutionFile; }
            set 
            { 
                _solutionFile = value;
                Text = $"ModelSync - {Path.GetFileName(_solutionFile)}";
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                _settings = SettingsBase.Load<Settings>();
                _settings.Position?.Apply(this);

                if (HasSolution(out string solutionFile)) LoadSolution(solutionFile);                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private bool HasSolution(out string solutionFile)
        {
            if (StartupArgs != null && File.Exists(StartupArgs[0]))
            {
                solutionFile = StartupArgs[0];
                return true;
            }

            solutionFile = null;
            return false;
        }

        private void LoadSolution(string fileName)
        {
            SuspendLayout();

            try
            {
                _solution = JsonFile.Load<Solution>(fileName);
                SolutionFile = fileName;
                tabMain.TabPages.Clear();

            }
            finally
            {
                ResumeLayout();
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
                var tab = new TabPage($"merge {tabMain.TabPages.Count}");
                var ui = new SyncUI() { Dock = DockStyle.Fill };
                tab.Controls.Add(ui);
                tabMain.TabPages.Insert(lastIndex, tab);
                tabMain.SelectedIndex = lastIndex;
            }
        }
    }
}
