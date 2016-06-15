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
    /// Resolves method in which will be injected ensures expressions
    /// </summary>
    public interface IEnsuresResolver
    {
        /// <summary>
        /// Resolves method in which will be injected ensures expressions by ensures definition
        /// </summary>
        /// <param name="ensuresDefinition">Ensures definition for which resolves method</param>
        /// <returns>Method in which will be injected ensures expressions</returns>
        MethodDefinition Resolve(EnsuresDefinition ensuresDefinition);
    }
}
