using ModelSync.App.Models;
using ModelSync.Library.Models;
using ModelSync.Library.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
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
                _binder.AddItems(cbSourceType, m => m.SourceType, SourceTypes);
                _binder.Add(tbSource, m => m.Source);
                _binder.Add(tbDest, m => m.Destination);
                // todo: color
                // todo: ignored objects

                _binder.Document = value;
            }
        }

        public async Task GenerateScriptAsync()
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
                    var objTypeNode = new ObjectTypeNode(typeGrp.Key, typeGrp.Count());
                    actionNode.Nodes.Add(objTypeNode);

                    foreach (var scriptAction in typeGrp.OrderBy(scr => scr.Object.Name))
                    {
                        var objNode = new ScriptActionNode(scriptAction);
                        objTypeNode.Nodes.Add(objNode);
                    }
                }
            }

            tvObjects.ExpandAll();

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

            var sourceType = SourceTypeValues[cbSourceType.SelectedItem as string];

            var connectionStrings = await GetSolutionConnectionStringsAsync(SolutionPath);

            switch (sourceType)
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
                tbScriptOutput.Text = new SqlServerDialect().FormatScript(nodes.Select(node => node.ScriptAction));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private async void cbSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadSuggestionsAsync();
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
    }
}
