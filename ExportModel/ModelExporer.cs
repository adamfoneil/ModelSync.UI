using ModelSync.Services;
using System;
using System.Reflection;

namespace ModelSync.CLI
{
    public static class ModelExporer
    {
        public static void Execute(string[] args)
        {
            try
            {
                var options = new ExportModelArgs(args);
                var assembly = Assembly.LoadFrom(options.Assembly);
                var dataModel = new AssemblyModelBuilder().GetDataModel(assembly, options.DefaultSchema, options.DefaultIdentityColumn);
                dataModel.SaveJson(options.OutputFile);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"ModelExporter error: {exc.Message}");
            }
        }
    }
}
