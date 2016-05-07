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
    public class FieldParameterBuilder : IInstructionsBuilder
    {
        private readonly FieldDefinition fieldDefinition;

        public FieldParameterBuilder(FieldDefinition fieldDefinition)
        {
            Contract.Requires(fieldDefinition != null);

            this.fieldDefinition = fieldDefinition;
        }

        public IEnumerable<Instruction> Build(IEnumerable<Instruction> instructions)
        {
            return EnumerableUtils.Concat(
                Enumerable.Repeat(Instruction.Create(OpCodes.Nop), 1),
                instructions,
                Enumerable.Repeat(Instruction.Create(OpCodes.Ldfld, fieldDefinition), 1));
        }
    }
}
