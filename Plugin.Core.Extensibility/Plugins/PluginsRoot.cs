using System.Collections;
using System.Collections.Generic;

namespace Plugin.Core.Extensibility.Plugins
{
    public class PluginsRoot
    {
        public string Root { get; set; }

        public IEnumerable<string> Plugins { get; set; }
    }
}