using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser
{
    public class BrowserOfAssembly
    {
        public BrowsingResult Browse(string assemblyPath)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var types = new List<Type>(assembly.GetTypes());

            var browsingResult = new BrowsingResult();
            var namespaces = new Dictionary<string, List<Type>>();
            foreach (var type in types)
            {
                var namespaceName = type.Namespace ?? "";
                if (!namespaces.TryGetValue(namespaceName, out var namespacesTypes))
                {
                    namespacesTypes = new List<Type>();
                    namespaces.Add(namespaceName, namespacesTypes);
                }

                namespacesTypes.Add(type);
            }

            foreach (KeyValuePair<string, List<Type>> namespaceType in namespaces)
            {
                var assemblyNamespace = new AssemblyNamespace(namespaceType.Key);
                foreach (var type in namespaceType.Value)
                {
                    var assemblyType = new AssemblyType(type);
                    assemblyNamespace.AssemblyTypes.Add(assemblyType);
                }

                browsingResult.Namespaces.Add(assemblyNamespace);
            }

            return browsingResult;
        }
    }
}