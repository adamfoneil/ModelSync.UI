using System.Collections.Generic;

namespace ModelSync.App.Models
{
    public class TestCase
    {
        public List<string> SqlCommands { get; set; }
        public bool IsCorrect { get; set; }
        public string Comments { get; set; }
    }
}
