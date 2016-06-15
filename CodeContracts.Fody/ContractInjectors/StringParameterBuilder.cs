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
    /// Creates il instructions for inject constant string to contract's validate method
    /// </summary>
    public class StringParameterBuilder : IParameterBuilder
    {
        /// <summary>
        /// Injected constant string
        /// </summary>
        private readonly string stringParameter;

        /// <summary>
        /// Initializes a new instance of class <see cref="StringParameterBuilder"/>
        /// </summary>
        /// <param name="stringParameter">Injected constant string</param>
        public StringParameterBuilder(string stringParameter)
        {
            Contract.Requires(stringParameter != null);
            
            this.stringParameter = stringParameter;
        }
        
        /// <inheritdoc/>
        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Ldstr, stringParameter);
        }
    }
}
