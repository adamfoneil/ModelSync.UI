using ModelSync.App.Models;
using ModelSync.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms.Library;
using System.Linq;
using ModelSync.Library.Abstract;
using ModelSync.Library.Services;

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

                tvObjects.Nodes.Clear();

                var rootNode = new TreeNode("SQL Script") { ImageKey = "script", SelectedImageKey = "script" };
                tvObjects.Nodes.Add(rootNode);

                var show = diff.Where(scr => !_binder.Document.IgnoreObjects?.Contains(scr.Object.Name) ?? true);

                Dictionary<ActionType, string> actionTypeIcons = new Dictionary<ActionType, string>()
                {
                    { ActionType.Create, "create" },
                    { ActionType.Alter, "update" },
                    { ActionType.Drop, "delete" }
                };

                Dictionary<ObjectType, string> objectTypeIcons = new Dictionary<ObjectType, string>()
                {
                    { ObjectType.Schema, "schema" },
                    { ObjectType.Table, "table" },
                    { ObjectType.Column, "column" },
                    { ObjectType.Index, "key" },
                    { ObjectType.ForeignKey, "shortcut" }
                };

                foreach (var actionGrp in show.GroupBy(scr => scr.Type).Select(grp => new { grp.Key, Icon = actionTypeIcons[grp.Key], Items = grp }))
                {
                    var actionNode = new TreeNode(actionGrp.Key.ToString()) 
                    { 
                        ImageKey = actionGrp.Icon, 
                        SelectedImageKey = actionGrp.Icon 
                    };
                    rootNode.Nodes.Add(actionNode);

                    foreach (var typeGrp in actionGrp.Items.GroupBy(scr => scr.Object.ObjectType))
                    {
                        var objTypeNode = new TreeNode($"{typeGrp.Key.ToString()} ({typeGrp.Count()})")
                        {
                            ImageKey = objectTypeIcons[typeGrp.Key],
                            SelectedImageKey = objectTypeIcons[typeGrp.Key]
                        };
                        actionNode.Nodes.Add(objTypeNode);

                        foreach (var obj in typeGrp.OrderBy(scr => scr.Object.Name))
                        {
                            var objNode = new TreeNode(obj.Object.Name) 
                            { 
                                ImageKey = objTypeNode.ImageKey, 
                                SelectedImageKey = objTypeNode.SelectedImageKey 
                            };
                            objTypeNode.Nodes.Add(objNode);
                        }
                    }
                }
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
                    return await new SqlServerModelBuilder().GetDataModelAsync(cn);
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
                var assembly = Assembly.LoadFile(text);
                var result = new AssemblyModelBuilder().GetDataModel(assembly);
                return await Task.FromResult(result);
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
