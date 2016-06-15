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
    /// Creates il instructions for inject parameter to contract's validate method
    /// </summary>
    public interface IParameterBuilder
    {
        /// <summary>
        /// Creates il instructions for inject specified parameter to contract's validate method
        /// </summary>
        /// <param name="validateParameterDefinition">Parameter of contract's validate method</param>
        /// <returns>Collection of created il instructions</returns>
        IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition);
    }
}
