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
    public class PropertyParameterBuilder : IInstructionsBuilder
    {
        private readonly PropertyDefinition propertyDefinition;

        public PropertyParameterBuilder(PropertyDefinition propertyDefinition)
        {
            Contract.Requires(propertyDefinition != null);
            Contract.Requires(propertyDefinition.GetMethod != null);

            this.propertyDefinition = propertyDefinition;
        }

        public IEnumerable<Instruction> Build(IEnumerable<Instruction> instructions)
        {
            return EnumerableUtils.Concat(
                Enumerable.Repeat(Instruction.Create(OpCodes.Nop), 1),
                instructions,
                Enumerable.Repeat(Instruction.Create(OpCodes.Callvirt, propertyDefinition.GetMethod), 1));
        }
    }
}
