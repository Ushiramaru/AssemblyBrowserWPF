using System.Collections.Generic;
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
            
            MethodAttributes attr = methodInfo.Attributes;

            Modifier = AccessModifier.Public;
            if ((attr & MethodAttributes.Public) != MethodAttributes.Public)
            {
                Modifier = AccessModifier.Private;
            }

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