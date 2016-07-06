using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Injects il instructions for invariant expression in specified method
    /// </summary>
    public interface IInvariantInjector
    {
        /// <summary>
        /// Injects il instructions for invariant expression in specified method
        /// </summary>
        /// <param name="invariantDefinition">Invariant definition for injecting to method</param>
        /// <param name="methodDefinition">Method in that will be injected il instructions</param>
        void Inject(InvariantDefinition invariantDefinition, MethodDefinition methodDefinition);
    }
}
