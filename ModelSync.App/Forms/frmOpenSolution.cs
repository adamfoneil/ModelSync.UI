using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinForms.Library;
using WinForms.Library.Extensions.ComboBoxes;
using WinForms.Library.Models;

namespace ModelSync.App.Forms
{
    public partial class frmOpenSolution : Form
    {
        public frmOpenSolution()
        {
            InitializeComponent();
        }

        public string SolutionFolder { get; set; }

        public string SelectedFilename 
        {
            get { return cbSolution.GetValue<string>(); }
        }

        private void frmOpenSolution_Load(object sender, EventArgs e)
        {
            try
            {
                cbSolution.Fill(FindSolutions());
                llSolutionFolder.Text = SolutionFolder;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private IEnumerable<ListItem<string>> FindSolutions()
        {
            List<ListItem<string>> results = new List<ListItem<string>>();
            FileSystem.EnumFiles(SolutionFolder, "*.sln", fileFound: (fi) =>
            {
                results.Add(new ListItem<string>(fi.FullName, fi.FullName.Substring(SolutionFolder.Length) + 1));
                return EnumFileResult.Continue;
            });
            return results;
        }

        private void llSolutionFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SolutionFolder = dlg.SelectedPath;
                llSolutionFolder.Text = dlg.SelectedPath;
                cbSolution.Fill(FindSolutions());
            }
        }
    }
}
