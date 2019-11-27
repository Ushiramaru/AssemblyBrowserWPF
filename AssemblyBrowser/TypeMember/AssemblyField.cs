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

            var attr = fieldInfo.Attributes;
            var accessAttributes = attr & FieldAttributes.FieldAccessMask;
            switch (accessAttributes)
            {
                case FieldAttributes.Private:
                    Modifier = AccessModifier.Private;
                    break;
                case FieldAttributes.Public:
                    Modifier = AccessModifier.Public;
                    break;
                case FieldAttributes.Family:
                    Modifier = AccessModifier.Protected;
                    break;
            }

            IsReadonly = (attr & FieldAttributes.InitOnly) != 0;
            IsStatic = (attr & FieldAttributes.Static) != 0;
        }

//        public bool IsStatic { get; }
//        public string AccesModifier { get; }
//        public string TypeName { get; }
//        public string Name { get; }
//
//
//        public AssemblyField(FieldInfo field)
//        {
//            IsStatic = field.IsStatic;
//            
//            TypeName = field.FieldType.Name;
//            Name = field.Name;
//        }
    }
}