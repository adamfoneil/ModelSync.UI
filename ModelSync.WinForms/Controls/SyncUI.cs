﻿using ModelSync.Abstract;
using ModelSync.App.Forms;
using ModelSync.App.Models;
using ModelSync.Interfaces;
using ModelSync.Models;
using ModelSync.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using WinForms.Library;
using WinForms.Library.Controls;
using WinForms.Library.Models;

namespace ModelSync.App.Controls
{
    public delegate IEnumerable<ExcludeAction> GetExcludeActionsHandler(string mergeName);
    public delegate void AddSharedExcludeActionHandler(string mergeName, ExcludeAction action);
    public delegate void RemoveSharedExcludeActionHandler(string mergeName, ExcludeAction action);

    public partial class SyncUI : UserControl
    {
        private TreeNode _selectedNode;
        private bool _allowExclude;
        private bool _allowInclude;
        private bool _manualEdits;

        private DataModel _sourceModel;
        private DataModel _destModel;
        private List<ScriptAction> _diff;

        public ControlBinder<MergeDefinition> _binder;

        public event EventHandler OperationStarted;
        public event EventHandler OperationComplete;
        public event EventHandler ScriptExecuted;
        public event EventHandler ScriptModified;
        public event EventHandler ScriptGenerated;
        public event GetExcludeActionsHandler GetSharedExclusions;
        public event AddSharedExcludeActionHandler AddSharedExclusion;
        public event RemoveSharedExcludeActionHandler RemoveSharedExclusion;

        public Func<string, IDbConnection> GetConnection { get; set; }
        public SqlDialect SqlDialect { get; set; }

        internal IEnumerable<ScriptActionNode> ScriptActions
        {
            get
            {
                List<ScriptActionNode> results = new List<ScriptActionNode>();

                addChildren(tvObjects.Nodes[0]);

                return results;

                void addChildren(TreeNode parent)
                {
                    var nodes = parent.Nodes.OfType<ScriptActionNode>();
                    results.AddRange(nodes);
                    foreach (TreeNode child in parent.Nodes) addChildren(child);
                };
            }
        }

        public SyncUI()
        {
            InitializeComponent();
        }

        public string SolutionPath { get; set; }

        private static Dictionary<SourceType, string> SourceTypes => new Dictionary<SourceType, string>()
        { 
            [SourceType.Assembly] = "From Assembly",
            [SourceType.Connection] = "From Connection",
            [SourceType.JsonFile] = "From Json File"
        };

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
                (_binder.Document.SourceType == SourceType.JsonFile) ? await GetModelFromFileAsync(tbSource.Text) :
                throw new Exception($"Unknown source type {_binder.Document.Source}");

            _destModel = await GetConnectionModelAsync(tbDest.Text);
            _diff = DataModel.Compare(_sourceModel, _destModel).ToList();

            tvObjects.Nodes.Clear();

            if (_binder.Document.ExcludeActions == null) _binder.Document.ExcludeActions = new List<ExcludeAction>();

            var sharedExclusions = GetSharedExclusions?.Invoke(_binder.Document.Title) ?? Enumerable.Empty<ExcludeAction>();

            var include = _diff.Where(scr => !_binder.Document.ExcludeActions.Contains(scr.GetExcludeAction()) && !sharedExclusions.Contains(scr.GetExcludeAction()));

            var scriptRoot = new TreeNode($"SQL Script ({include.Count()})") { ImageKey = "script", SelectedImageKey = "script" };
            tvObjects.Nodes.Add(scriptRoot);

            if (_sourceModel.Errors?.Any() ?? false)
            {
                var errorNode = new TreeNode($"Errors ({_sourceModel.Errors.Count})") { ImageKey = "warning", SelectedImageKey = "warning" };
                foreach (var err in _sourceModel.Errors) errorNode.Nodes.Add(new TreeNode($"{err.Key.Name}: {err.Value}") { ImageKey = "warning", SelectedImageKey = "warning" });
                tvObjects.Nodes.Add(errorNode);
            }

            LoadScriptActions(scriptRoot, include, (action) =>
            {
                var result = new ScriptActionNode(action);
                if (result.ActionRequired) result.NodeFont = new Font(tvObjects.Font, FontStyle.Bold);
                return result;
            });

            int excludeCount = _binder.Document.ExcludeActions.Count + sharedExclusions.Count();
            var excludeRoot = new TreeNode($"Exclude ({excludeCount})") { ImageKey = "exclude", SelectedImageKey = "exclude" };
            tvObjects.Nodes.Add(excludeRoot);

            if (_binder.Document.ExcludeActions.Any())
            {                
                LoadScriptActions(excludeRoot, _binder.Document.ExcludeActions, (action) => new ExcludeActionNode(action));
            }            

            if (sharedExclusions.Any())
            {
                var sharedExcludeNode = new RepoNode($"Shared ({sharedExclusions.Count()})");
                excludeRoot.Nodes.Add(sharedExcludeNode);
                LoadScriptActions(sharedExcludeNode, sharedExclusions, (action) => new ExcludeActionNode(action));
            }

            scriptRoot.ExpandAll();
            tvObjects.SelectedNode = tvObjects.Nodes[0];

            ScriptGenerated?.Invoke(this, new EventArgs());
        }

        private static void LoadScriptActions<T>(TreeNode rootNode, IEnumerable<T> actions, Func<T, TreeNode> nodeCreator) where T : IActionable
        {
            var actionTypeIcons = new Dictionary<ActionType, string>()
            {
                [ActionType.Create] = "create",
                [ActionType.Alter] = "update",
                [ActionType.Drop] = "delete"
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

                    foreach (var action in typeGrp)
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
                _manualEdits = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async Task<DataModel> GetModelFromFileAsync(string text)
        {
            try
            {
                OperationStarted?.Invoke("Reading model data...", new EventArgs());
                
                var model = DataModel.FromJsonFile(text);
                return await Task.FromResult(model);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error reading model data: {exc.Message}", exc);
            }
            finally
            {
                OperationComplete?.Invoke(this, new EventArgs());
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
                UpdateSqlScript(e.Node);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// updates the script output without triggering manual edit event
        /// </summary>
        private void SetGeneratedSql(string sql)
        {
            _manualEdits = false;
            tbScriptOutput.TextChanged -= tbScriptOutput_TextChanged;
            tbScriptOutput.Clear();
            tbScriptOutput.Text = sql;
            tbScriptOutput.TextChanged += tbScriptOutput_TextChanged;
        }

        private void UpdateSqlScript(TreeNode node)
        {
            List<ScriptActionNode> nodes = new List<ScriptActionNode>();
            var thisAction = node as ScriptActionNode;
            if (thisAction != null) nodes.Add(thisAction);

            void addChildrenR(TreeNode parent)
            {
                nodes.AddRange(parent.Nodes.OfType<ScriptActionNode>());
                foreach (TreeNode child in parent.Nodes) addChildrenR(child);
            };

            addChildrenR(node);
            
            SetGeneratedSql(SqlDialect.FormatScript(nodes.Select(nodeInner => nodeInner.ScriptAction)));
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            await ExecuteInner(e, "Executing script...", true);
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            await ExecuteInner(e, "Testing script...", false, "Script tested successfully; changes rolled back.");
        }

        private async Task ExecuteInner(EventArgs e, string statusBarMessage, bool commit, string successMessage = null)
        {
            try
            {
                OperationStarted?.Invoke(statusBarMessage, e);

                using (var cn = GetConnection.Invoke(tbDest.Text))
                {
                    await SqlDialect.ExecuteAsync(cn, tbScriptOutput.Text, commit);

                    if (commit)
                    {
                        ScriptExecuted?.Invoke(this, new EventArgs());
                        if (_manualEdits) MessageBox.Show("Changes applied successfully.", "Script Executed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (!string.IsNullOrEmpty(successMessage)) MessageBox.Show(successMessage, "SQL Test", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (!_manualEdits && commit) await GenerateScriptAsync();
                }
            }
            catch (Exception exc)
            {
                if (MessageBox.Show(exc.Message + "\r\n\r\nClick OK to create test case.", "Script Error", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    frmSaveTestCase.PromptSaveZipFile(false, exc.Message, _sourceModel, _destModel, _diff);
                }
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
                    try
                    {
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
                    catch 
                    {
                        // some kind of json error, have to ignore
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

                case SourceType.JsonFile:
                    tbSource.SelectFile("Json Files|*.json", e);
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
            excludeSharedToolStripMenuItem.Enabled = _allowExclude;
            setDefaultToolStripMenuItem.Enabled = ((_selectedNode as ScriptActionNode)?.ScriptAction?.Object as Column)?.DefaultValueRequired ?? false;
        }

        private void ExcludeChildObjects(TreeNode node, bool shared)
        {
            Dictionary<bool, Action<ExcludeAction>> storageActions = new Dictionary<bool, Action<ExcludeAction>>()
            {
                [true] = (action) => AddSharedExclusion?.Invoke(_binder.Document.Title, action),
                [false] = (action) => _binder.Document.ExcludeActions.Add(action)
            };

            if (node is ScriptActionNode)
            {
                var action = (node as ScriptActionNode).ScriptAction.GetExcludeAction();
                storageActions[shared].Invoke(action);
            }
            else
            {
                foreach (TreeNode child in node.Nodes) ExcludeChildObjects(child, shared);
            }
        }

        private void IncludeChildObjects(TreeNode node)
        {            
            if (node is ExcludeActionNode excludeNode)
            {
                var action = (node as ExcludeActionNode).ExcludeAction;

                switch (excludeNode.Scope)
                {
                    case ExcludeActionScope.Local:
                        _binder.Document.ExcludeActions.Remove(action);
                        break;

                    case ExcludeActionScope.Shared:
                        RemoveSharedExclusion.Invoke(_binder.Document.Title, action);
                        break;
                }                               
            }
            else
            {
                foreach (TreeNode child in node.Nodes) IncludeChildObjects(child);
            }
        }

        private async void excludeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcludeChildObjects(_selectedNode, false);
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
                    dlg.PromptSaveZipFile(_sourceModel, _destModel, _diff);
                }                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void setDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var actionNode = _selectedNode as ScriptActionNode;
            var currentValue = (actionNode?.ScriptAction?.Object as Column)?.DefaultValue;

            var dlg = new frmPromptText()
            {
                Text = "Set Column Default",
                NewText = currentValue
            };

            if (dlg.ShowDialog() == DialogResult.OK)
            {                
                if (actionNode != null)
                {
                    var column = actionNode.ScriptAction.Object as Column;
                    if (column != null) column.DefaultValue = dlg.NewText;
                    UpdateSqlScript(actionNode);
                    actionNode.NodeFont = new Font(tvObjects.Font, FontStyle.Regular);
                }                
            }
        }

        private void tbScriptOutput_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            _manualEdits = true;
            ScriptModified?.Invoke(sender, e);
        }

        private async void excludeSharedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcludeChildObjects(_selectedNode, true);
            await GenerateScriptAsync();
        }
    }
}
