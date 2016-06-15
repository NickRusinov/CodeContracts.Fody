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
    /// Resolves method in which will be injected requires expressions
    /// </summary>
    public interface IRequiresResolver
    {
        /// <summary>
        /// Resolves method in which will be injected requires expressions by requires definition
        /// </summary>
        /// <param name="requiresDefinition">Requires definition for which resolves method</param>
        /// <returns>Method in which will be injected requires expressions</returns>
        MethodDefinition Resolve(RequiresDefinition requiresDefinition);
    }
}
