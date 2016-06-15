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
    /// Resolves type which is a contract class for specified type
    /// </summary>
    public interface IContractClassResolver
    {
        /// <summary>
        /// Resolves type which is a contract class for specified type and its method
        /// </summary>
        /// <param name="typeDefinition">Type for which resolves a contract class</param>
        /// <param name="methodDefinition">
        /// Method of type for which resolves a contract class for requires or ensures expressions; 
        /// or null for invariants, because invariants defined for type generally
        /// </param>
        /// <returns>Contract class for specified type</returns>
        TypeDefinition Resolve(TypeDefinition typeDefinition, MethodDefinition methodDefinition);
    }
}
