using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractParameterBuilders
{
    public class TypeParameterBuilder : IParameterBuilder
    {
        private readonly ModuleDefinition moduleDefinition;

        private readonly TypeReference typeReferenceParameter;

        public TypeParameterBuilder(ModuleDefinition moduleDefinition, TypeReference typeReferenceParameter)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(typeReferenceParameter != null);

            this.moduleDefinition = moduleDefinition;
            this.typeReferenceParameter = typeReferenceParameter;
        }

        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            var getTypeFromHandleMethodReference = typeof(Type).GetMethod("GetTypeFromHandle");

            yield return Instruction.Create(OpCodes.Ldtoken, typeReferenceParameter);
            yield return Instruction.Create(OpCodes.Call, moduleDefinition.ImportReference(getTypeFromHandleMethodReference));
        }
    }
}
