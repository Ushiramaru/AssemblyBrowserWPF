using System;

namespace AssemblyBrowser.TypeMember
{
    public abstract class AssemblyTypeMember
    {
        public string Name { get; protected set; }
        public Type ValueType { get; protected set; }
    }
}