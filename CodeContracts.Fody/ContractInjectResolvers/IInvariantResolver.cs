using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectResolvers
{
    /// <summary>
    /// Resolves method in which will be injected invariant expressions
    /// </summary>
    public interface IInvariantResolver
    {
        /// <summary>
        /// Resolves method in which will be injected invariant expressions by invariant definition
        /// </summary>
        /// <param name="invariantDefinition">Invariant definition for which resolves method</param>
        /// <returns>Method in which will be injected invariant expressions</returns>
        MethodDefinition Resolve(InvariantDefinition invariantDefinition);
    }
}
