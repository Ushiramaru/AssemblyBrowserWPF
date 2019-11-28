using System.Reflection;
using AssemblyBrowser.Enums;

namespace AssemblyBrowser.TypeMember
{
    public class AssemblyField : AssemblyTypeMember
    {
        public bool IsReadonly { get; }
        public AccessModifier Modifier { get; }
        public bool IsStatic { get; }

        public AssemblyField(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            ValueType = fieldInfo.FieldType;

            if (fieldInfo.IsPublic)
            {
                Modifier = AccessModifier.Public;
            }

            if (fieldInfo.IsPrivate)
            {
                Modifier = AccessModifier.Private;
            }

            if (fieldInfo.IsFamily)
            {
                Modifier = AccessModifier.Protected;
            }

            if (fieldInfo.IsAssembly)
            {
                Modifier = AccessModifier.Internal;
            }

            var attr = fieldInfo.Attributes;
            IsReadonly = (attr & FieldAttributes.InitOnly) != 0;
            IsStatic = (attr & FieldAttributes.Static) != 0;
        }
    }
}