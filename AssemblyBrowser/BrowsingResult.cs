using System.Collections.Generic;

namespace AssemblyBrowser
{
    public class BrowsingResult
    {
        public List<AssemblyNamespace> Namespaces { get; }
        public BrowsingResult()
        {
            Namespaces = new List<AssemblyNamespace>();
        }
    }
}