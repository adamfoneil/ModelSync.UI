using ModelSync.App.Models;
using ModelSync.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Library;

namespace ModelSync.App.Controls
{    
    public partial class SyncUI : UserControl
    {
        public ControlBinder<MergeDefinition> _binder;

        public event EventHandler OperationStarted;
        public event EventHandler OperationComplete;

        public Func<string, IDbConnection> GetConnection { get; set; }

        public SyncUI()
        {
            InitializeComponent();            
        }

        public string SolutionPath { get; set; }

        public MergeDefinition Document
        {
            get { return _binder.Document; }
            set
            {
                _binder = new ControlBinder<MergeDefinition>();                
                _binder.AddItems(cbSourceType, m => m.SourceType, new Dictionary<SourceType, string>()
                {
                    { SourceType.Assembly, "From Assembly" },
                    { SourceType.Connection, "From Connection" }
                });
                _binder.Add(tbSource, m => m.Source);
                _binder.Add(tbDest, m => m.Destination);
                // todo: color
                // todo: ignored objects

                _binder.Document = value;
            }
        }
        
        private async void btnGenerateScript_Click(object sender, EventArgs e)
        {
            try
            {
                DataModel sourceModel =
                    (_binder.Document.SourceType == SourceType.Assembly) ? await GetAssemblyModelAsync(tbSource.Text) :
                    (_binder.Document.SourceType == SourceType.Connection) ? await GetConnectionModelAsync(tbSource.Text) :
                    throw new Exception($"Unknown source type {_binder.Document.Source}");

                DataModel destModel = await GetConnectionModelAsync(tbDest.Text);
                var diff = DataModel.Compare(sourceModel, destModel);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async Task<DataModel> GetConnectionModelAsync(string text)
        {
            try
            {
                OperationStarted?.Invoke("Analyzing database...", new EventArgs());
                using (var cn = GetConnection(text))
                {
                    return await DataModel.FromSqlServerAsync(cn);
                }
            }
            catch (Exception exc)
            {
                throw new Exception($"Error analyzing database: {exc.Message}", exc);
            }
            finally
            {
                OperationComplete?.Invoke(this, new EventArgs());
            }
        }

        private async Task<DataModel> GetAssemblyModelAsync(string text)
        {
            try
            {
                OperationStarted?.Invoke("Analyzing assembly...", new EventArgs());
                //var assembly = Assembly.ReflectionOnlyLoadFrom(text);                                
                var assembly = Assembly.LoadFrom(text);
                return await DataModel.FromAssemblyAsync(assembly);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error analyzing assembly: {exc.Message}");
            }
            finally
            {
                OperationComplete?.Invoke(this, new EventArgs());
            }
        }
    }
}
