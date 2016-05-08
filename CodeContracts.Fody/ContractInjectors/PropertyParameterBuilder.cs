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
    public class PropertyParameterBuilder : IParameterBuilder
    {
        private readonly PropertyDefinition propertyDefinition;

        public PropertyParameterBuilder(PropertyDefinition propertyDefinition)
        {
            Contract.Requires(propertyDefinition != null);
            Contract.Requires(propertyDefinition.GetMethod != null);

            this.propertyDefinition = propertyDefinition;
        }

        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Callvirt, propertyDefinition.GetMethod);
        }
    }
}
