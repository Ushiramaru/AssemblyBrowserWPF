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
        public readonly List<MethodInfo> AutoMethods;

        public AssemblyProperty(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            ValueType = propertyInfo.PropertyType;

            AutoMethods = new List<MethodInfo>();

            CanRead = propertyInfo.CanRead;
            if (CanRead)
            {
                var method = propertyInfo.GetMethod;
                AutoMethods.Add(method);
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
                AutoMethods.Add(method);
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

            foreach (var methodInfo in AutoMethods)
            {
                IsVirtual = methodInfo.IsVirtual;
                IsAbstract = methodInfo.IsAbstract;
                IsStatic = methodInfo.IsStatic;
            }

            if (GetterModifier == AccessModifier.Public || SetterModifier == AccessModifier.Public)
            {
                Modifier = AccessModifier.Public;
            }
            else if (GetterModifier == AccessModifier.Protected || SetterModifier == AccessModifier.Protected)
            {
                Modifier = AccessModifier.Protected;
            }
            else if (GetterModifier == AccessModifier.Internal || SetterModifier == AccessModifier.Internal)
            {
                Modifier = AccessModifier.Internal;
            }
            else
            {
                Modifier = AccessModifier.Private;
            }
        }
    }
}