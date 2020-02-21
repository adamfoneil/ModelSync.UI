using JsonSettings;
using JsonSettings.Library;
using ModelSync.App.Controls;
using ModelSync.App.Models;
using System;
using System.IO;
using System.Linq;
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

                if (LoadSolutionOnStartup())
                {
                    _solution = LoadSolution();
                }
                else
                {
                    _solution = new Solution()
                    {
                        Merges = new MergeDefinition[] { new MergeDefinition() }.ToList()
                    };
                    syncUI1.Document = _solution.Merges[0];
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private bool LoadSolutionOnStartup()
        {
            if (StartupArgs?.Length > 0)
            {
                string baseFile = Path.GetFileNameWithoutExtension(StartupArgs[0]);

                SolutionFile = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "ModelSync", baseFile + ".json");

                return File.Exists(SolutionFile);
            }

            SolutionFile = null;
            return false;
        }

        private Solution LoadSolution()
        {
            // todo: save current solution

            SuspendLayout();

            try
            {
                var solution = JsonFile.Load<Solution>(SolutionFile);
                if (solution.Merges.Any())
                {
                    tabMain.TabPages.Clear();
                    foreach (var merge in _solution.Merges)
                    {
                        var tab = new TabPage(merge.Title);
                        var ui = new SyncUI() { Dock = DockStyle.Fill, Document = merge };
                        tab.Controls.Add(ui);
                        tabMain.TabPages.Add(tab);
                    }
                }

                return solution;
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

                if (_solution != null)
                {
                    JsonFile.Save(SolutionFile, _solution);
                }                
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
