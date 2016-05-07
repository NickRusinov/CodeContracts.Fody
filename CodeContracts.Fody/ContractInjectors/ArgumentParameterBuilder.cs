using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ArgumentParameterBuilder : IInstructionsBuilder
    {
        private readonly ParameterDefinition parameterDefinition;

        public ArgumentParameterBuilder(ParameterDefinition parameterDefinition)
        {
            Contract.Requires(parameterDefinition != null);

            this.parameterDefinition = parameterDefinition;
        }

        public IEnumerable<Instruction> Build(IEnumerable<Instruction> instructions)
        {
            return EnumerableUtils.Concat(
                Enumerable.Repeat(Instruction.Create(OpCodes.Nop), 1),
                instructions,
                Enumerable.Repeat(Instruction.Create(OpCodes.Ldarg, parameterDefinition), 1));
        }
    }
}
