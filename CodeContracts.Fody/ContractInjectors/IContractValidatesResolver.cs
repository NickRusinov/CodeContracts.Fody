using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Resolves collection of validate methods for injecting to specified method
    /// </summary>
    public interface IContractValidatesResolver
    {
        /// <summary>
        /// Resolves collection of validate methods from specified definition of contract and 
        /// method to that will be injected its validation methods
        /// </summary>
        /// <param name="contractDefinition">Specified definition of contract</param>
        /// <param name="methodDefinition">Method to that will be injected resolved validation methods</param>
        /// <returns>Collection of methods of validation of contract</returns>
        IEnumerable<ContractValidate> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition);
    }
}
