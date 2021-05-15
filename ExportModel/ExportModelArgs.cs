using System.IO;

namespace ModelSync.CLI
{
    public class ExportModelArgs : ArgParser
    {
        public ExportModelArgs(string[] args) : base(args)
        {
            Assembly = Get(0);
            OutputFile = Get(1, DefaultOutputFile(Assembly));
            DefaultSchema = Get(2, "dbo");
            DefaultIdentityColumn = Get(3, "Id");
        }

        private string DefaultOutputFile(string assemblyName)
        {
            string path = Path.GetDirectoryName(assemblyName);
            string fileName = Path.GetFileNameWithoutExtension(assemblyName) + ".ModelSync.json";
            return Path.Combine(path, fileName);
        }

        public string Assembly { get; }
        public string OutputFile { get; }
        public string DefaultSchema { get; }
        public string DefaultIdentityColumn { get; }
    }
}
