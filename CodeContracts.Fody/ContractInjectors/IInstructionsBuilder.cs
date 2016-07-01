using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
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
