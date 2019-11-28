using System.Collections.Generic;
using AssemblyBrowser;
using AssemblyBrowser.TypeMember;

namespace AssemblyBrowserDesktop.Model
{
    public class Browser
    {
        public BrowserOfAssembly BrowserOfAssembly { get; }

        public Browser()
        {
            BrowserOfAssembly = new BrowserOfAssembly();
        }

        public List<TreeNode> Browse(string assemblyPath)
        {
            BrowsingResult browsingResult = BrowserOfAssembly.Browse(assemblyPath);
            return GetNodesList(browsingResult);
        }

        private List<TreeNode> GetNodesList(BrowsingResult browseResult)
        {
            List<TreeNode> result = new List<TreeNode>();
            foreach (AssemblyNamespace assemblyNamespace in browseResult.Namespaces)
            {
                var namespaceNode = new TreeNode(assemblyNamespace.Name);

                foreach (AssemblyType asmType in assemblyNamespace.AssemblyTypes)
                {
                    var typeNode = new TreeNode(Converter.ConvertToString(asmType));
                    foreach (AssemblyField field in asmType.Fields)
                    {
                        var elementNode = new TreeNode(Converter.ConvertToString(field));
                        typeNode.Nodes.Add(elementNode);
                    }

                    foreach (AssemblyProperty property in asmType.Properties)
                    {
                        var propNode = new TreeNode(Converter.ConvertToString(property));
                        typeNode.Nodes.Add(propNode);
                    }

                    foreach (AssemblyConstructor ctor in asmType.Constructors)
                    {
                        var methodNode = new TreeNode(Converter.ConvertToString(ctor));
                        typeNode.Nodes.Add(methodNode);
                    }

                    foreach (AssemblyMethod method in asmType.Methods)
                    {
                        var methodNode = new TreeNode(Converter.ConvertToString(method));
                        typeNode.Nodes.Add(methodNode);
                    }

                    namespaceNode.Nodes.Add(typeNode);
                }

                result.Add(namespaceNode);
            }

            return result;
        }
    }
}