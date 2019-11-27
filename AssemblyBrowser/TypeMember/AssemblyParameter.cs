using System;
using System.Reflection;
using AssemblyBrowser.Enums;

namespace AssemblyBrowser.TypeMember
{
    public class AssemblyParameter
    {
        public string Name { get; }
        public Type ValueType { get; }
        public ParameterType PassingType { get; }
        public bool IsOptional { get; }

        public AssemblyParameter(ParameterInfo parameter)
        {
            Name = parameter.Name;
            ValueType = parameter.ParameterType;

            if (parameter.IsIn)
            {
                PassingType = ParameterType.In;
            }
            else if (parameter.IsOut)
            {
                PassingType = ParameterType.Out;
            }

            IsOptional = parameter.IsOptional;
        }
    }
}