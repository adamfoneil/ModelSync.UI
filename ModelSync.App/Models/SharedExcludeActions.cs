using ModelSync.Models;
using System.Collections.Generic;

namespace ModelSync.App.Models
{
    /// <summary>
    /// exclude actions saved with the repo
    /// </summary>
    public class SharedExcludeActions
    {
        public Dictionary<string, List<ExcludeAction>> Actions { get; set; }
    }
}
