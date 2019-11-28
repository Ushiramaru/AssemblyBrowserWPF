using System.Collections.Generic;

namespace AssemblyBrowserDesktop.Model
{
    public class TreeNode
    {
        public string Name { get; }
        public IList<TreeNode> Nodes { get; }

        public TreeNode(string name)
        {
            Name = name;
            Nodes = new List<TreeNode>();
        }
    }
}