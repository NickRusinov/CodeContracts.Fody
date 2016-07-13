using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractValidateResolvers;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInstructionsBuilders
{
    /// <summary>
    /// Creates il instructions for injected method of contract attribute
    /// </summary>
    public interface IInstructionsBuilder
    {
        /// <summary>
        /// Creates il instructions for inject specified method of contract attribute
        /// </summary>
        /// <param name="contractValidate">Method of contract attribute</param>
        /// <returns>Collection of created il instructions</returns>
        IEnumerable<Instruction> Build(ContractValidate contractValidate);
    }
}
