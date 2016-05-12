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
    public class FieldParameterBuilder : IParameterBuilder
    {
        private readonly FieldDefinition fieldDefinition;

        public FieldParameterBuilder(FieldDefinition fieldDefinition)
        {
            Contract.Requires(fieldDefinition != null);

            this.fieldDefinition = fieldDefinition;
        }

        public TypeReference ParameterType => fieldDefinition.FieldType;

        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Ldfld, fieldDefinition);
        }
    }
}
