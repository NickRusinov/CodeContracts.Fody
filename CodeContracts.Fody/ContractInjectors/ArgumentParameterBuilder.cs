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
    public class ArgumentParameterBuilder : IParameterBuilder
    {
        private readonly ParameterDefinition parameterDefinition;

        public ArgumentParameterBuilder(ParameterDefinition parameterDefinition)
        {
            Contract.Requires(parameterDefinition != null);

            this.parameterDefinition = parameterDefinition;
        }

        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Ldarg, parameterDefinition);
        }
    }
}
