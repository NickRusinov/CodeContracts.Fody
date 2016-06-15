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
    /// <summary>
    /// Creates il instructions for inject field to contract's validate method
    /// </summary>
    public class FieldParameterBuilder : IParameterBuilder
    {
        /// <summary>
        /// Injected field
        /// </summary>
        private readonly FieldDefinition fieldDefinition;

        /// <summary>
        /// Initializes a new instance of class <see cref="FieldParameterBuilder"/>
        /// </summary>
        /// <param name="fieldDefinition">Injected field</param>
        public FieldParameterBuilder(FieldDefinition fieldDefinition)
        {
            Contract.Requires(fieldDefinition != null);

            this.fieldDefinition = fieldDefinition;
        }
        
        /// <inheritdoc/>
        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Ldfld, fieldDefinition);
        }
    }
}
