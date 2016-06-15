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
    /// Resolves method in which will be injected contract (requires, ensures or invariant) expressions
    /// </summary>
    public interface IContractInjectResolver
    {
        /// <summary>
        /// Resolves method in which will be injected contract (requires, ensures or invariant) expressions 
        /// by contract (requires, ensures or invariant) definition
        /// </summary>
        /// <param name="contractDefinition">
        /// Contract (requries, ensures or invariant) definition for which resolves method
        /// </param>
        /// <returns>
        /// Method in which will be injected contract (requries, ensures or invariant) expressions
        /// </returns>
        MethodDefinition Resolve(ContractDefinition contractDefinition);
    }
}
