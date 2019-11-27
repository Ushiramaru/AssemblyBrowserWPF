using System.Collections.Generic;
using System.Reflection;
using AssemblyBrowser.Enums;

namespace AssemblyBrowser.TypeMember
{
    public class AssemblyProperty : AssemblyTypeMember
    {
        public bool CanRead { get; }
        public bool CanWrite { get; }
        public bool IsAbstract { get; }
        public bool IsVirtual { get; }
        public AccessModifier SetterModifier { get; }
        public AccessModifier GetterModifier { get; }
        public AccessModifier Modifier { get; }
        public bool IsStatic { get; }

        public AssemblyProperty(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            ValueType = propertyInfo.PropertyType;

            var methodsList = new List<MethodInfo>();

            CanRead = propertyInfo.CanRead;
            if (CanRead)
            {
                var method = propertyInfo.GetMethod;
                methodsList.Add(method);
                if (method.IsPublic)
                {
                    GetterModifier = AccessModifier.Public;
                }
                else if (method.IsPrivate)
                {
                    GetterModifier = AccessModifier.Private;
                }
                else if (method.IsAssembly)
                {
                    GetterModifier = AccessModifier.Internal;
                }
                else if (method.IsFamily)
                {
                    GetterModifier = AccessModifier.Protected;
                }
            }

            CanWrite = propertyInfo.CanWrite;
            if (CanWrite)
            {
                var method = propertyInfo.SetMethod;
                methodsList.Add(method);
                if (method.IsPublic)
                {
                    SetterModifier = AccessModifier.Public;
                }
                else if (method.IsPrivate)
                {
                    SetterModifier = AccessModifier.Private;
                }
                else if (method.IsAssembly)
                {
                    SetterModifier = AccessModifier.Internal;
                }
                else if (method.IsFamily)
                {
                    SetterModifier = AccessModifier.Protected;
                }
            }

            foreach (var methodInfo in methodsList)
            {
                IsVirtual = methodInfo.IsVirtual;
                IsAbstract = methodInfo.IsAbstract;
                IsStatic = methodInfo.IsStatic;
            }

            if (ValueType.IsNestedPublic)
            {
                Modifier = AccessModifier.Public;
            }
            else if (ValueType.IsNestedPrivate)
            {
                Modifier = AccessModifier.Private;
            }
            else if (ValueType.IsNestedAssembly)
            {
                Modifier = AccessModifier.Internal;
            }
            else if (ValueType.IsNestedFamily)
            {
                Modifier = AccessModifier.Protected;
            }
        }

//        public List<MethodInfo> PropertyMethods(PropertyInfo propertyInfo)
//        {
//            var propertyMethods = new List<MethodInfo>();
//            if (CanRead)
//            {
//                propertyMethods.Add(propertyInfo.GetMethod);
//            }
//            if (CanWrite)
//            {
//                propertyMethods.Add(propertyInfo.SetMethod);
//            }
//
//            return propertyMethods;
//        }
    }
}