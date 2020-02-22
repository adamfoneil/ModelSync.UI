using JsonSettings;
using JsonSettings.Library;
using ModelSync.App.Controls;
using ModelSync.App.Forms;
using ModelSync.App.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private MouseEventArgs _tabRightClick;

        public string[] StartupArgs { get; set; }       

        public frmMain()
        {
            InitializeComponent();
        }

        public TabControl TabControl { get { return tabMain; } }
        
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

                _solution = (LoadSolutionOnStartup(StartupArgs, out string fileName)) ?
                    JsonFile.Load<Solution>(fileName) :
                    Solution.Create();

                if (!string.IsNullOrEmpty(fileName))
                {
                    SolutionFile = fileName;
                }                

                LoadSolution(this, _solution, fileName);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private static bool LoadSolutionOnStartup(string[] startupArgs, out string fileName)
        {
            if (startupArgs?.Length > 0)
            {
                string baseFile = Path.GetFileNameWithoutExtension(startupArgs[0]);

                fileName = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "ModelSync", baseFile + ".json");

                return File.Exists(fileName);
            }

            fileName = null;
            return false;
        }

        private void LoadSolution(frmMain form, Solution solution, string solutionFile)
        {
            // todo: save current solution

            form.TabControl.TabIndexChanged -= form.tabMain_SelectedIndexChanged;
            form.SuspendLayout();

            string solutionPath = (!string.IsNullOrEmpty(solutionFile)) ?
                Path.GetDirectoryName(solutionFile) :
                null;

            try
            {                
                if (solution?.Merges.Any() ?? false)
                {
                    int index = 0;
                    foreach (var merge in solution.Merges)
                    {
                        var tab = new TabPage(merge.Title ?? $"merge {index}") 
                        { 
                            ImageKey = GetTabImage(merge.SourceType),
                            BackColor = merge.BackgroundColor
                        };

                        var ui = new SyncUI() 
                        { 
                            Dock = DockStyle.Fill, 
                            Document = merge,
                            SolutionPath = solutionPath,                            
                        };
                        ui.OperationStarted += StartOperation;
                        ui.OperationComplete += CompleteOperation;
                        ui.GetConnection = (text) => new SqlConnection(text);

                        tab.Controls.Add(ui);
                        form.TabControl.TabPages.Insert(index, tab);
                        index++;
                    }
                    form.TabControl.SelectedIndex = 0;
                }
            }
            finally
            {
                form.TabControl.TabIndexChanged += form.tabMain_SelectedIndexChanged;
                form.ResumeLayout();
            }            
        }

        private void CompleteOperation(object sender, EventArgs e)
        {
            tslStatus.Text = "Ready";
            pbMain.Visible = false;
        }

        private void StartOperation(object sender, EventArgs e)
        {
            tslStatus.Text = sender.ToString();
            pbMain.Visible = true;
        }

        private static string GetTabImage(SourceType sourceType)
        {
            Dictionary<SourceType, string> icons = new Dictionary<SourceType, string>()
            {
                { SourceType.Assembly, "assembly" },
                { SourceType.Connection, "connection" }
            };

            return icons[sourceType];
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _settings.Position = FormPosition.FromForm(this);
                _settings.Save();

                if (!string.IsNullOrEmpty(SolutionFile) && _solution != null)
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
                var merge = new MergeDefinition();
                _solution.Merges.Add(merge);

                var tab = new TabPage($"merge {tabMain.TabPages.Count}");
                var ui = new SyncUI() { Dock = DockStyle.Fill, Document = merge };
                tab.Controls.Add(ui);
                tabMain.TabPages.Insert(lastIndex, tab);
                tabMain.SelectedIndex = lastIndex;
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tab = GetSelectedTab(tabMain);

            var dlg = new frmRename()
            {
                RenameText = tab.Text
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {                
                tab.Text = dlg.RenameText;
                (tab.Controls[0] as SyncUI).Document.Title = dlg.RenameText;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tab = GetSelectedTab(tabMain);
            var merge = (tab.Controls[0] as SyncUI).Document;
            if (MessageBox.Show($"This will delete the merge '{merge.Title}'.", "Delete Merge", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                tabMain.TabPages.Remove(tab);
                _solution.Merges.Remove(merge);
            }
        }

        private void setColorToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            var dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var tab = GetSelectedTab(tabMain);
                var merge = (tab.Controls[0] as SyncUI).Document;
                merge.BackgroundColor = dlg.Color;
                tab.BackColor = dlg.Color;
            }
        }

        private void tabMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) _tabRightClick = e;
        }

        /// <summary>
        /// thanks to https://social.msdn.microsoft.com/Forums/windows/en-US/e09d081d-a7f5-479d-bd29-44b6d163ebc8/tabcontrol-how-to-capture-mouse-rightclick-on-tab?forum=winforms
        /// </summary>
        private TabPage GetSelectedTab(TabControl tabControl)
        {
            if (_tabRightClick == null) return tabControl.SelectedTab;

            TabPage result = null;
            for (int i = 0; i < tabControl.TabPages.Count; i++)
            {
                var rect = tabControl.GetTabRect(i);
                if (rect.Contains(_tabRightClick.Location))
                {
                    result = tabControl.TabPages[i];
                    break;
                }
            }

            _tabRightClick = null;
            return result;
        }

    }
}
