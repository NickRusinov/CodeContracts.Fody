using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody
{
    /// <summary>
    /// Top level algorithm that do nothing
    /// </summary>
    public class DisabledContractExecutor : IContractExecutor
    {
        /// <inheritdoc/>
        public void Execute(ModuleDefinition moduleDefinition)
        {

        }
    }
}
