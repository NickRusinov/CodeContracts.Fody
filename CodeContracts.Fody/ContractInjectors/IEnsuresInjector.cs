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
    /// Injects il instructions for ensures expression in specified method
    /// </summary>
    public interface IEnsuresInjector
    {
        /// <summary>
        /// Injects il instructions for ensures expression in specified method
        /// </summary>
        /// <param name="ensuresDefinition">Ensures definition for injecting to method</param>
        /// <param name="methodDefinition">Method in that will be injected il instructions</param>
        void Inject(EnsuresDefinition ensuresDefinition, MethodDefinition methodDefinition);
    }
}
