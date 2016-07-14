using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody
{
    /// <summary>
    /// Top level algorithm for replace custom contract attributes to <see cref="Contract"/>'s methods calls
    /// </summary>
    public interface IContractExecutor
    {
        /// <summary>
        /// Execute top level algorithm with specified assembly that represented by <see cref="ModuleDefinition"/>
        /// </summary>
        /// <param name="moduleDefinition">Assembly that be handled and rewrited</param>
        void Execute(ModuleDefinition moduleDefinition);
    }
}
