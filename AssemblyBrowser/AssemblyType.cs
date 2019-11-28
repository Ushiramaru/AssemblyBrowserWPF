using System;
using System.Collections.Generic;
using System.Reflection;
using AssemblyBrowser.Enums;
using AssemblyBrowser.TypeMember;

namespace AssemblyBrowser
{
    public class AssemblyType
    {
        private const BindingFlags BindingFlags = System.Reflection.BindingFlags.Instance |
                                                  System.Reflection.BindingFlags.Public |
                                                  System.Reflection.BindingFlags.NonPublic |
                                                  System.Reflection.BindingFlags.Static;

        public string Name { get; }
        public bool IsNested { get; }
        public bool IsClass { get; }
        public bool IsStructure { get; }
        public bool IsSealed { get; }
        public bool IsInterface { get; }
        public bool IsAbstract { get; }
        public AccessModifier Modifier { get; }
        public List<AssemblyField> Fields { get; }
        public List<AssemblyProperty> Properties { get; }
        public List<AssemblyMethod> Methods { get; }
        public List<AssemblyConstructor> Constructors { get; }
        public bool IsEnum { get; }

        public AssemblyType(Type type)
        {
            Name = type.Name;
            Fields = new List<AssemblyField>();
            Properties = new List<AssemblyProperty>();
            Methods = new List<AssemblyMethod>();
            Constructors = new List<AssemblyConstructor>();
            var autoMethods = new List<MethodInfo>();

            IsNested = type.IsNested;
            IsSealed = type.IsSealed;

            TypeAttributes visibilityMask = type.Attributes & TypeAttributes.VisibilityMask;
            switch (visibilityMask)
            {
                case TypeAttributes.Public:
                    Modifier = AccessModifier.Public;
                    break;
                case TypeAttributes.NotPublic:
                    Modifier = AccessModifier.Private;
                    break;
                case TypeAttributes.NestedAssembly:
                    Modifier = AccessModifier.Internal;
                    break;
                case TypeAttributes.NestedFamily:
                    Modifier = AccessModifier.Protected;
                    break;
            }

            TypeAttributes classSemantics = type.Attributes & TypeAttributes.ClassSemanticsMask;
            switch (classSemantics)
            {
                case TypeAttributes.Class:
                    IsAbstract = (type.Attributes & TypeAttributes.Abstract) != 0;
                    IsClass = type.IsClass;
                    if (!IsClass)
                    {
                        IsStructure = true;
                        IsEnum = type.IsEnum;
                    }

                    break;
                case TypeAttributes.Interface:
                    IsInterface = true;
                    Modifier = AccessModifier.Public;
                    break;
            }

            PropertyInfo[] properties = type.GetProperties(BindingFlags);
            foreach (var property in properties)
            {
                var assemblyProperty = new AssemblyProperty(property);
                autoMethods.AddRange(assemblyProperty.AutoMethods);
                Properties.Add(assemblyProperty);
            }

            FieldInfo[] fields = type.GetFields(BindingFlags);
            foreach (var field in fields)
            {
                if (field.Name.Contains("<")) continue;
                var assemblyField = new AssemblyField(field);
                Fields.Add(assemblyField);
            }

            MethodInfo[] methods = type.GetMethods(BindingFlags);
            foreach (var methodInfo in methods)
            {
                if (!autoMethods.Contains(methodInfo))
                {
                    var method = new AssemblyMethod(methodInfo);
                    Methods.Add(method);
                }
            }

            ConstructorInfo[] constructors = type.GetConstructors(BindingFlags);
            foreach (var constructorInfo in constructors)
            {
                var constructor = new AssemblyConstructor(constructorInfo);
                Constructors.Add(constructor);
            }
        }
    }
}