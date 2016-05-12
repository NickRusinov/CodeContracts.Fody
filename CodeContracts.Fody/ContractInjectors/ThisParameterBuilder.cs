using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ThisParameterBuilder : IParameterBuilder
    {
        private readonly TypeDefinition typeDefinition;

        public ThisParameterBuilder(TypeDefinition typeDefinition)
        {
            Contract.Requires(typeDefinition != null);

            this.typeDefinition = typeDefinition;
        }

        public TypeReference ParameterType => typeDefinition;

        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Ldarg_0);
        }
    }
}
