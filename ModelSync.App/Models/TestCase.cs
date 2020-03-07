using ModelSync.Library.Models;
using System.Collections.Generic;

namespace ModelSync.App.Models
{
    public class TestCase
    {
        public DataModel SourceModel { get; set; }
        public DataModel DestModel { get; set; }
        public List<string> SqlCommands { get; set; }
        public bool IsCorrect { get; set; }
        public string Comments { get; set; }
    }
}
