using ModelSync.App.Forms;
using ModelSync.App.Models;
using ModelSync.Library.Abstract;
using ModelSync.Library.Interfaces;
using ModelSync.Library.Models;
using ModelSync.Library.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using WinForms.Library;
using WinForms.Library.Controls;
using WinForms.Library.Models;

namespace ModelSync.App.Controls
{
    public partial class SyncUI : UserControl
    {
        private TreeNode _selectedNode;
        private bool _allowExclude;
        private bool _allowInclude;

        private DataModel _sourceModel;
        private DataModel _destModel;
        private List<ScriptAction> _diff;

        public ControlBinder<MergeDefinition> _binder;

        public event EventHandler OperationStarted;
        public event EventHandler OperationComplete;
        public event EventHandler ScriptExecuted;

        public Func<string, IDbConnection> GetConnection { get; set; }
        public SqlDialect SqlDialect { get; set; }

        internal IEnumerable<ScriptActionNode> ScriptActions
        {
            get
            {
                List<ScriptActionNode> results = new List<ScriptActionNode>();

                void addChildren(TreeNode parent)
                {
                    var nodes = parent.Nodes.OfType<ScriptActionNode>();
                    results.AddRange(nodes);
                    foreach (TreeNode child in parent.Nodes) addChildren(child);
                };

                addChildren(tvObjects.Nodes[0]);

                return results;
            }
        }

        public SyncUI()
        {
            InitializeComponent();
        }

        public string SolutionPath { get; set; }

        private static Dictionary<SourceType, string> SourceTypes
        {
            get
            {
                return new Dictionary<SourceType, string>()
                {
                    { SourceType.Assembly, "From Assembly" },
                    { SourceType.Connection, "From Connection" }
                };
            }
        }

        private static Dictionary<string, SourceType> SourceTypeValues
        {
            get { return SourceTypes.ToDictionary(kp => kp.Value, kp => kp.Key); }
        }

        public MergeDefinition Document
        {
            get { return _binder.Document; }
            set
            {
                _binder = new ControlBinder<MergeDefinition>();
                _binder.PropertyUpdated += async delegate (object sender, MergeDefinition document, string propertyName)
                {
                    if (propertyName.Equals(nameof(MergeDefinition.SourceType)))
                    {
                        await LoadSuggestionsAsync();
                    }                    
                };
                _binder.AddItems(cbSourceType, m => m.SourceType, SourceTypes);
                _binder.Add(tbSource, m => m.Source);
                _binder.Add(tbDest, m => m.Destination);
                _binder.Document = value;
            }
        }

        public async Task GenerateScriptAsync()
        {
            if (string.IsNullOrEmpty(tbSource.Text)) return;
            if (string.IsNullOrEmpty(tbDest.Text)) return;

            _sourceModel =
                (_binder.Document.SourceType == SourceType.Assembly) ? await GetAssemblyModelAsync(tbSource.Text) :
                (_binder.Document.SourceType == SourceType.Connection) ? await GetConnectionModelAsync(tbSource.Text) :
                throw new Exception($"Unknown source type {_binder.Document.Source}");

            _destModel = await GetConnectionModelAsync(tbDest.Text);
            _diff = DataModel.Compare(_sourceModel, _destModel).ToList();

            tvObjects.Nodes.Clear();

            if (_binder.Document.ExcludeActions == null) _binder.Document.ExcludeActions = new List<ExcludeAction>();

            var include = _diff.Where(scr => !_binder.Document.ExcludeActions.Contains(scr.GetExcludeAction()));

            var scriptRoot = new TreeNode($"SQL Script ({include.Count()})") { ImageKey = "script", SelectedImageKey = "script" };
            tvObjects.Nodes.Add(scriptRoot);

            LoadScriptActions(scriptRoot, include, (action) => new ScriptActionNode(action));

            if (_binder.Document.ExcludeActions.Any())
            {
                var excludeRoot = new TreeNode($"Exclude ({_binder.Document.ExcludeActions.Count})") { ImageKey = "exclude", SelectedImageKey = "exclude" };
                tvObjects.Nodes.Add(excludeRoot);
                LoadScriptActions(excludeRoot, _binder.Document.ExcludeActions, (action) => new ExcludeActionNode(action));
            }

            scriptRoot.ExpandAll();
            tvObjects.SelectedNode = tvObjects.Nodes[0];
        }

        private static void LoadScriptActions<T>(TreeNode rootNode, IEnumerable<T> actions, Func<T, TreeNode> nodeCreator) where T : IActionable
        {
            Dictionary<ActionType, string> actionTypeIcons = new Dictionary<ActionType, string>()
            {
                { ActionType.Create, "create" },
                { ActionType.Alter, "update" },
                { ActionType.Drop, "delete" }
            };

            foreach (var actionGrp in actions.GroupBy(scr => scr.Type).Select(grp => new { grp.Key, Icon = actionTypeIcons[grp.Key], Items = grp }))
            {
                var actionNode = new TreeNode(actionGrp.Key.ToString())
                {
                    ImageKey = actionGrp.Icon,
                    SelectedImageKey = actionGrp.Icon
                };
                rootNode.Nodes.Add(actionNode);

                foreach (var typeGrp in actionGrp.Items.GroupBy(scr => scr.ObjectType))
                {
                    var objTypeNode = new ObjectTypeNode(typeGrp.Key, typeGrp.Count());
                    actionNode.Nodes.Add(objTypeNode);

                    foreach (var action in typeGrp.OrderBy(scr => scr.ObjectName))
                    {
                        var objNode = nodeCreator.Invoke(action);
                        objTypeNode.Nodes.Add(objNode);
                    }
                }
            }
        }

        private async void btnGenerateScript_Click(object sender, EventArgs e)
        {
            try
            {
                await GenerateScriptAsync();
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
                // help from https://docs.microsoft.com/en-us/dotnet/framework/deployment/best-practices-for-assembly-loading
                OperationStarted?.Invoke("Analyzing assembly...", new EventArgs());
                var assembly = Assembly.LoadFrom(text);
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

        public async Task LoadSuggestionsAsync()
        {
            if (string.IsNullOrEmpty(SolutionPath)) return;            

            var connectionStrings = await GetSolutionConnectionStringsAsync(SolutionPath);

            switch (_binder.Document.SourceType)
            {
                case SourceType.Assembly:
                    tbSource.Suggestions = await LoadAssemblySuggestionsAsync();
                    break;

                case SourceType.Connection:
                    tbSource.Suggestions = connectionStrings;
                    break;
            }

            tbDest.Suggestions = connectionStrings;
        }

        private void tvObjects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                List<ScriptActionNode> nodes = new List<ScriptActionNode>();
                var thisAction = e.Node as ScriptActionNode;
                if (thisAction != null) nodes.Add(thisAction);

                void addChildrenR(TreeNode parent)
                {
                    nodes.AddRange(parent.Nodes.OfType<ScriptActionNode>());
                    foreach (TreeNode child in parent.Nodes) addChildrenR(child);
                };

                addChildrenR(e.Node);

                tbScriptOutput.Clear();
                tbScriptOutput.Text = SqlDialect.FormatScript(nodes.Select(node => node.ScriptAction));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                OperationStarted?.Invoke("Executing script...", e);

                using (var cn = GetConnection.Invoke(tbDest.Text))
                {
                    await SqlDialect.ExecuteAsync(cn, tbScriptOutput.Text);
                    await GenerateScriptAsync();
                    ScriptExecuted?.Invoke(this, new EventArgs());
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                OperationComplete?.Invoke(this, new EventArgs());
            }
        }

        public async Task<IEnumerable<ListItem<string>>> LoadAssemblySuggestionsAsync()
        {
            var assemblies = await GetSolutionAssemblies(SolutionPath);
            var uniqueFiles = PathUtil.UniquifyFiles(assemblies);
            return uniqueFiles.Select(kp => new ListItem<string>(kp.Value, kp.Key));
        }

        private static async Task<IEnumerable<string>> GetSolutionAssemblies(string solutionPath)
        {
            List<string> projects = new List<string>();

            await Task.Run(() =>
            {
                FileSystem.EnumFiles(solutionPath, "*.csproj", fileFound: (fi) =>
                {
                    projects.Add(fi.FullName);
                    return EnumFileResult.NextFolder;
                });
            });

            IEnumerable<string> findAssemblies(string csprojFile)
            {
                XDocument doc = XDocument.Load(csprojFile);
                var assemblyName = doc.XPathSelectElement("/*[local-name()='Project']/*[local-name()='PropertyGroup']/*[local-name()='AssemblyName']");
                if (assemblyName != null)
                {
                    var builds = doc.XPathSelectElements("/*[local-name()='Project']/*[local-name()='PropertyGroup']/*[local-name()='OutputPath']");
                    foreach (var output in builds)
                    {
                        string value = Path.Combine(Path.GetDirectoryName(csprojFile), output.Value, assemblyName.Value + ".dll");
                        if (File.Exists(value)) yield return value;
                    }

                    var assemblyElement = doc.XPathSelectElement("/*[local-name()='Project']/*[local-name()='PropertyGroup']/*[local-name()='AssemblyName']");
                    if (assemblyElement != null)
                    {
                        List<string> results = new List<string>();
                        FileSystem.EnumFiles(Path.GetDirectoryName(csprojFile), "*.dll", fileFound: (fi) =>
                        {
                            if (Path.GetFileNameWithoutExtension(fi.FullName).Equals(assemblyElement.Value))
                            {
                                results.Add(fi.FullName);
                            }
                            return EnumFileResult.NextFolder;
                        });
                        foreach (var file in results) yield return file;
                    }
                }
            }

            return projects.SelectMany(csproj =>
            {
                return findAssemblies(csproj);
            });
        }

        private static async Task<IEnumerable<ListItem<string>>> GetSolutionConnectionStringsAsync(string solutionPath)
        {
            List<ListItem<string>> results = new List<ListItem<string>>();

            await Task.Run(() =>
            {
                FileSystem.EnumFiles(solutionPath, "*.json", fileFound: (fi) =>
                {
                    int added = findConnectionStringsInJson(fi.FullName);
                    return (added == 0) ? EnumFileResult.Continue : EnumFileResult.Stop;
                });

                FileSystem.EnumFiles(solutionPath, "*.config", fileFound: (fi) =>
                {
                    int added = findConnectionStringsInXml(fi.FullName);
                    return (added == 0) ? EnumFileResult.Continue : EnumFileResult.Stop;
                });
            });

            return results;

            int findConnectionStringsInJson(string fileName)
            {
                int result = 0;
                using (var reader = File.OpenText(fileName))
                {
                    string json = reader.ReadToEnd();
                    JObject @object = JsonConvert.DeserializeObject(json) as JObject;
                    if (@object != null)
                    {
                        foreach (var kp in @object)
                        {
                            if (kp.Key.Equals("ConnectionStrings"))
                            {
                                foreach (JProperty item in kp.Value)
                                {
                                    result++;
                                    results.Add(new ListItem<string>(item.Value.ToString(), item.Name));
                                }
                            }

                            if (kp.Key.StartsWith("ConnectionStrings:"))
                            {
                                result++;
                                results.Add(new ListItem<string>(kp.Value.ToString(), kp.Key));
                            }
                        }
                    }
                }
                return result;
            }

            int findConnectionStringsInXml(string fileName)
            {
                var doc = XDocument.Load(fileName);
                var elements = doc.XPathSelectElements("/configuration/connectionStrings/add");
                results.AddRange(elements.Select(e => new ListItem<string>(e.Attribute("connectionString").Value, e.Attribute("name").Value)));
                return elements.Count();
            }
        }

        private void tbSource_BuilderClicked(object sender, BuilderEventArgs e)
        {
            var sourceType = SourceTypeValues[cbSourceType.SelectedItem as string];

            switch (sourceType)
            {
                case SourceType.Assembly:
                    tbSource.SelectFile("Assemblies|*.dll", e);
                    break;

                case SourceType.Connection:
                    break;
            }
        }

        private void tvObjects_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _selectedNode = e.Node;
            _allowExclude = InheritsFrom("script", e.Node);
            _allowInclude = InheritsFrom("exclude", e.Node);
        }

        private bool InheritsFrom(string imageKey, TreeNode node)
        {
            if (node.ImageKey.Equals(imageKey)) return true;
            if (node.Parent == null) return false;
            return InheritsFrom(imageKey, node.Parent);
        }

        private void cmDiff_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            includeToolStripMenuItem.Enabled = _allowInclude;
            excludeToolStripMenuItem.Enabled = _allowExclude;
        }

        private void ExcludeChildObjects(TreeNode node)
        {
            if (node is ScriptActionNode)
            {
                var action = (node as ScriptActionNode).ScriptAction.GetExcludeAction();
                _binder.Document.ExcludeActions.Add(action);
            }
            else
            {
                foreach (TreeNode child in node.Nodes) ExcludeChildObjects(child);
            }
        }

        private void IncludeChildObjects(TreeNode node)
        {
            if (node is ExcludeActionNode)
            {
                var action = (node as ExcludeActionNode).ExcludeAction;
                _binder.Document.ExcludeActions.Remove(action);
            }
            else
            {
                foreach (TreeNode child in node.Nodes) IncludeChildObjects(child);
            }
        }

        private async void excludeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcludeChildObjects(_selectedNode);
            await GenerateScriptAsync();
        }

        private async void includeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IncludeChildObjects(_selectedNode);
            await GenerateScriptAsync();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbScriptOutput.Text);
        }

        private void ddbSave_ButtonClick(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "SQL Files|*.sql|All Files|*.*";
            dlg.DefaultExt = "sql";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dlg.FileName, tbScriptOutput.Text);
            }
        }

        private void testCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var dlg = new frmSaveTestCase();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var saveDlg = new SaveFileDialog();
                    saveDlg.Filter = "Zip files|*.zip|All Files|*.*";
                    saveDlg.DefaultExt = "zip";
                    if (saveDlg.ShowDialog() == DialogResult.OK)
                    {
                        var testCase = new TestCase()
                        {
                            SqlCommands = _diff.SelectMany(scr => scr.Commands).ToList(),
                            IsCorrect = dlg.IsCorrect,
                            Comments = dlg.Comments
                        };

                        using (var file = File.Create(saveDlg.FileName))
                        {
                            using (var zip = new ZipArchive(file, ZipArchiveMode.Create))
                            {
                                AddEntry(zip, "SourceModel.json", _sourceModel.ToJson());
                                AddEntry(zip, "DestModel.json", _destModel.ToJson());
                                AddEntry(zip, "TestCase.json", JsonConvert.SerializeObject(testCase, Formatting.Indented));
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void AddEntry(ZipArchive zip, string entryName, string content)
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
