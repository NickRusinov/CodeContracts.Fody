using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.Tests
{
    public static class ModuleDefinitionsExtensions
    {
        public static TypeDefinition FindType(this ModuleDefinition moduleDefinition, string typeName)
        {
            return moduleDefinition.Types.Single(td => td.Name == typeName);
        }

        public static FieldDefinition FindField(this ModuleDefinition moduleDefinition, string typeName, string fieldName)
        {
            return moduleDefinition.FindType(typeName).Fields.Single(fd => fd.Name == fieldName);
        }

        public static ParameterDefinition FindParameter(this ModuleDefinition moduleDefinition, string typeName, string methodName, string parameterName)
        {
            return moduleDefinition.FindMethod(typeName, methodName).Parameters.Single(pd => pd.Name == parameterName);
        }

        public static PropertyDefinition FindProperty(this ModuleDefinition moduleDefinition, string typeName, string propertyName)
        {
            return moduleDefinition.FindType(typeName).Properties.Single(pd => pd.Name == propertyName);
        }

        public static MethodDefinition FindMethod(this ModuleDefinition moduleDefinition, string typeName, string methodName)
        {
            return moduleDefinition.FindType(typeName).Methods.Single(md => md.Name == methodName);
        }
    }
}
