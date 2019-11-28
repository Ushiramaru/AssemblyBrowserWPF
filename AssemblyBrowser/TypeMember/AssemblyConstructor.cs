using System.Collections.Generic;
using System.Reflection;
using AssemblyBrowser.Enums;

namespace AssemblyBrowser.TypeMember
{
    public class AssemblyConstructor : AssemblyTypeMember
    {
        public new const string Name = "constructor";
        public AccessModifier Modifier { get; }
        public List<AssemblyParameter> Parameters { get; }
        public bool IsStatic { get; }

        public AssemblyConstructor(ConstructorInfo constructor)
        {
            if (constructor.IsPublic)
            {
                Modifier = AccessModifier.Public;
            }

            if (constructor.IsPrivate)
            {
                Modifier = AccessModifier.Private;
            }

            if (constructor.IsFamily)
            {
                Modifier = AccessModifier.Protected;
            }

            if (constructor.IsAssembly)
            {
                Modifier = AccessModifier.Internal;
            }

            ParameterInfo[] parameters = constructor.GetParameters();
            Parameters = new List<AssemblyParameter>();
            foreach (var parameter in parameters)
            {
                var newParameter = new AssemblyParameter(parameter);
                Parameters.Add(newParameter);
            }

            IsStatic = constructor.IsStatic;
        }
    }
}