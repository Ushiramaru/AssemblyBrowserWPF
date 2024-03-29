﻿using System.Collections.Generic;
using System.Reflection;
using AssemblyBrowser.Enums;

namespace AssemblyBrowser.TypeMember
{
    public class AssemblyMethod : AssemblyTypeMember
    {
        public List<AssemblyParameter> Parameters { get; }
        public AccessModifier Modifier { get; }
        public bool IsAbstract { get; }
        public bool IsVirtual { get; }
        public bool IsStatic { get; }

        public AssemblyMethod(MethodInfo methodInfo)
        {
            Name = methodInfo.Name;
            ValueType = methodInfo.ReturnType;

            if (methodInfo.IsPublic)
            {
                Modifier = AccessModifier.Public;
            }

            if (methodInfo.IsPrivate)
            {
                Modifier = AccessModifier.Private;
            }

            if (methodInfo.IsFamily)
            {
                Modifier = AccessModifier.Protected;
            }

            if (methodInfo.IsAssembly)
            {
                Modifier = AccessModifier.Internal;
            }

            MethodAttributes attr = methodInfo.Attributes;
            IsVirtual = (attr & MethodAttributes.Virtual) != 0;
            IsAbstract = (attr & MethodAttributes.Abstract) != 0;
            IsStatic = (attr & MethodAttributes.Static) != 0;


            Parameters = new List<AssemblyParameter>();
            ParameterInfo[] parameters = methodInfo.GetParameters();
            foreach (var parameter in parameters)
            {
                var newParameter = new AssemblyParameter(parameter);
                Parameters.Add(newParameter);
            }
        }
    }
}