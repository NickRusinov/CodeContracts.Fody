using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectResolvers
{
    /// <summary>
    /// Resolves method which is a invariant method for specified type
    /// </summary>
    public interface IInvariantMethodResolver
    {
        /// <summary>
        /// Resolves method which is a invariant method for specified type
        /// </summary>
        /// <param name="typeDefinition">Type for which resolves a invariant method</param>
        /// <returns>Invariant method for specified type</returns>
        MethodDefinition Resolve(TypeDefinition typeDefinition);
    }
}
