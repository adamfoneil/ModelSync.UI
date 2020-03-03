using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        public string[] SolutionFiles { get; set; }

        public string SelectedFilename 
        {
            get { return cbSolution.GetValue<string>(); }
        }

        private async void frmOpenSolution_Load(object sender, EventArgs e)
        {
            try
            {
                llSolutionFolder.Text = SolutionFolder;
                lblSolutionCount.Visible = false;

                var solutions = (SolutionFiles?.Any() ?? false) ? 
                    SolutionFiles.Select(fileName => new ListItem<string>(fileName, fileName.Substring(SolutionFolder.Length + 1))) :
                    await FindSolutionsAsync();

                cbSolution.Fill(solutions);                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async Task<IEnumerable<ListItem<string>>> FindSolutionsAsync()
        {
            List<ListItem<string>> results = new List<ListItem<string>>();

            IProgress<int> showProgress = new Progress<int>(ShowProgress);

            progressBar1.Visible = true;
            lblSolutionCount.Visible = true;

            await Task.Run(() =>
            {
                FileSystem.EnumFiles(SolutionFolder, "*.sln", fileFound: (fi) =>
                {
                    results.Add(new ListItem<string>(fi.FullName, fi.FullName.Substring(SolutionFolder.Length + 1)));
                    showProgress.Report(results.Count);
                    return EnumFileResult.NextFolder;
                });
            });
            
            progressBar1.Visible = false;
            lblSolutionCount.Visible = false;

            SolutionFiles = results.Select(li => li.Value).ToArray();

            return results;
        }

        private void ShowProgress(int obj)
        {
            lblSolutionCount.Text = $"{obj} solutions found";
        }

        private async void llSolutionFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                SolutionFolder = dlg.SelectedPath;
                var solutions = await FindSolutionsAsync();
                cbSolution.Fill(solutions);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbSolution.SelectedItem != null)
            {
                string fileName = cbSolution.GetValue<string>();
                if (File.Exists(fileName))
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    var fileList = SolutionFiles.ToList();
                    fileList.Remove(fileName);
                    SolutionFiles = fileList.ToArray();
                    MessageBox.Show($"The solution file {fileName} no longer exists, and will be removed.");
                }
            }
            else
            {
                MessageBox.Show("Please select a solution.");
            }
        }
    }
}
