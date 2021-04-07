using ModelSync.App.Models;
using ModelSync.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ModelSync.App.Forms
{
    public partial class frmSaveTestCase : Form
    {
        public frmSaveTestCase()
        {
            InitializeComponent();
        }

        public bool IsCorrect
        {
            get { return rbCorrect.Checked; }
        }

        public string Comments
        {
            get { return tbIncorrectNotes.Text; }
        }

        public Exception Exception { get; set; }

        private void rbCorrect_CheckedChanged(object sender, System.EventArgs e)
        {
            tbIncorrectNotes.Enabled = !rbCorrect.Checked;
        }

        public void PromptSaveZipFile(DataModel sourceModel, DataModel destModel, List<ScriptAction> diff)
        {
            PromptSaveZipFile(rbCorrect.Checked, tbIncorrectNotes.Text, sourceModel, destModel, diff);
        }

        public static void PromptSaveZipFile(bool isCorrect, string comments, DataModel sourceModel, DataModel destModel, List<ScriptAction> diff)
        {            
            var saveDlg = new SaveFileDialog();
            saveDlg.Filter = "Zip files|*.zip|All Files|*.*";
            saveDlg.DefaultExt = "zip";
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                var testCase = new TestCase()
                {
                    SqlCommands = diff.SelectMany(scr => scr.CommandsWithoutComments()).ToList(),
                    IsCorrect = isCorrect,
                    Comments = comments
                };

                using (var file = File.Create(saveDlg.FileName))
                {
                    using (var zip = new ZipArchive(file, ZipArchiveMode.Create))
                    {
                        addEntry(zip, "SourceModel.json", sourceModel.ToJson());
                        addEntry(zip, "DestModel.json", destModel.ToJson());
                        addEntry(zip, "TestCase.json", JsonConvert.SerializeObject(testCase, Formatting.Indented));
                    }
                }
            }
            
            void addEntry(ZipArchive zip, string entryName, string content)
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                var entry = zip.CreateEntry(entryName);
                using (var entryStream = entry.Open())
                {
                    entryStream.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}
