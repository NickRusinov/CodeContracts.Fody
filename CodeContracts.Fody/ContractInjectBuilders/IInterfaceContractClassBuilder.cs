using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectBuilders
{
    /// <summary>
    /// Creates a new contract class for specified type which is interface
    /// </summary>
    public interface IInterfaceContractClassBuilder
    {
        /// <summary>
        /// Creates a new contract class for specified type which is interface
        /// </summary>
        /// <param name="typeDefinition">Type for which creates a contract class</param>
        /// <returns>Contract class for specified type</returns>
        TypeDefinition Build(TypeDefinition typeDefinition);
    }
}
