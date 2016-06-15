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
    /// Creates il instructions for inject method's parameter to contract's validate method
    /// </summary>
    public class ArgumentParameterBuilder : IParameterBuilder
    {
        /// <summary>
        /// Injected parameter of method
        /// </summary>
        private readonly ParameterDefinition parameterDefinition;

        /// <summary>
        /// Initializes a new instance of class <see cref="ArgumentParameterBuilder"/>
        /// </summary>
        /// <param name="parameterDefinition">Injected parameter of method</param>
        public ArgumentParameterBuilder(ParameterDefinition parameterDefinition)
        {
            Contract.Requires(parameterDefinition != null);

            this.parameterDefinition = parameterDefinition;
        }
        
        /// <inheritdoc/>
        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Ldarg, parameterDefinition);
        }
    }
}
