using System.Collections.Generic;

namespace AssemblyBrowser
{
    public class AssemblyNamespace
    {
        public string Name { get; }
        public List<AssemblyType> AssemblyTypes { get; }

        public AssemblyNamespace(string name)
        {
            Name = name;
            AssemblyTypes = new List<AssemblyType>();
        }
    }
}