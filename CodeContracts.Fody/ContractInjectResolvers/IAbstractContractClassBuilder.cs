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
    /// Creates a new contract class for specified type which is abstract class
    /// </summary>
    public interface IAbstractContractClassBuilder
    {
        /// <summary>
        /// Creates a new contract class for specified type which is abstract class
        /// </summary>
        /// <param name="typeDefinition">Type for which creates a contract class</param>
        /// <returns>Contract class for specified type</returns>
        TypeDefinition Build(TypeDefinition typeDefinition);
    }
}
