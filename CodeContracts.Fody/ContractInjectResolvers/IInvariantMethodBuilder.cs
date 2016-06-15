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
    /// Creates a new invariant method for specified type
    /// </summary>
    public interface IInvariantMethodBuilder
    {
        /// <summary>
        /// Creates a new invariamt method for specified type
        /// </summary>
        /// <param name="typeDefinition">Type for which creates a new invariant method</param>
        /// <returns>Invariant method for specified type</returns>
        MethodDefinition Build(TypeDefinition typeDefinition);
    }
}
