﻿using JsonSettings;
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

                string solutionFolder = _settings.SolutionFolder;
                string[] solutionFiles = _settings.SolutionFiles?.ToArray() ?? new string[0];

                bool exit = false;
                if (GetSolutionFile(StartupArgs, ref solutionFiles, ref solutionFolder, out string fileName))
                {                    
                    LoadSolution(fileName);
                }
                else
                {
                    // user didn't pick a solution, so we have to exit
                    exit = true;
                }

                _settings.SolutionFolder = solutionFolder;
                _settings.SolutionFiles = solutionFiles.ToList();

                if (exit)
                {
                    Close();
                    Application.Exit();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private static bool GetSolutionFile(string[] startupArgs, ref string[] cachedSolutionFiles, ref string solutionFolder, out string fileName)
        {
            if (startupArgs?.Length > 0)
            {
                fileName = Solution.GetFilename(startupArgs[0]);
                return true;
            }
            else
            {
                frmOpenSolution dlg = new frmOpenSolution()
                {
                    SolutionFolder = solutionFolder,
                    SolutionFiles = cachedSolutionFiles
                };

                while (true)
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        fileName = Solution.GetFilename(dlg.SelectedFilename);
                        solutionFolder = dlg.SolutionFolder;
                        cachedSolutionFiles = dlg.SolutionFiles;
                        return true;
                    }
                    else
                    {
                        solutionFolder = dlg.SolutionFolder;
                        cachedSolutionFiles = dlg.SolutionFiles;
                        if (MessageBox.Show("Please select a solution or Cancel to exit.", "Open Solution", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            break;
                        }
                    }
                }                
            }

            fileName = null;
            return false;
        }

        private void LoadSolution(string fileName)
        {
            SaveCurrentSolution();            
            
            tabMain.TabIndexChanged -= tabMain_SelectedIndexChanged;
            SuspendLayout();            
                
            try
            {
                Solution result = (File.Exists(fileName)) ? 
                    JsonFile.Load<Solution>(fileName) :
                    Solution.Create();                

                string solutionPath = Path.GetDirectoryName(fileName);                                
                
                int index = 0;
                foreach (var merge in result.Merges)
                {
                    var tab = new MergeDefinitionTab(fileName, merge.Title ?? $"merge {index}") 
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
                    tabMain.TabPages.Insert(index, tab);
                    index++;
                }

                // remove tab pages from the prior solution
                List<TabPage> removePages = new List<TabPage>();
                foreach (var tab in tabMain.TabPages)
                {
                    var mergeTab = tab as MergeDefinitionTab;
                    if (mergeTab != null && !mergeTab.SolutionFile.Equals(fileName)) removePages.Add(mergeTab);
                }
                foreach (var tab in removePages) tabMain.TabPages.Remove(tab);

                tabMain.SelectedIndex = 0;

                SolutionFile = fileName;
                _solution = result;
            }
            finally
            {
                tabMain.TabIndexChanged += tabMain_SelectedIndexChanged;
                ResumeLayout();
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
                SaveCurrentSolution();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void SaveCurrentSolution()
        {
            if (!string.IsNullOrEmpty(SolutionFile) && _solution != null)
            {
                JsonFile.Save(SolutionFile, _solution);
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
            
            return result;
        }

        private void llOpenSolution_Click(object sender, EventArgs e)
        {
            try
            {
                frmOpenSolution dlg = new frmOpenSolution()
                {
                    SolutionFiles = _settings.SolutionFiles?.ToArray(),
                    SolutionFolder = _settings.SolutionFolder
                };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    LoadSolution(Solution.GetFilename(dlg.SelectedFilename));
                }

                _settings.SolutionFolder = dlg.SolutionFolder;
                _settings.SolutionFiles = dlg.SolutionFiles?.ToList();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void cmTabControls_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var tab = GetSelectedTab(tabMain);
            bool enabled = !tab.Text.Equals("new sync");
            renameToolStripMenuItem.Enabled = enabled;
            deleteToolStripMenuItem.Enabled = enabled;
            setColorToolStripMenuItem.Enabled = enabled;
        }
    }
}
