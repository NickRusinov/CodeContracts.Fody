using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractParameterBuilders
{
    /// <summary>
    /// Creates il instructions for inject null reference to contract's validate method
    /// </summary>
    public class NullParameterBuilder : IParameterBuilder
    {
        /// <inheritdoc/>
        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Ldnull);
        }
    }
}
