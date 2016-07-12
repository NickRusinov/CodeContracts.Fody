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
    /// Resolves validate method for injecting to one of many <see cref="Contract"/> class methods
    /// </summary>
    public interface IContractValidateResolver
    {
        /// <summary>
        /// Resolves validate method from specified definition of contract and collection of parameters 
        /// for current resolving method
        /// </summary>
        /// <param name="contractDefinition">Specified definition of contract</param>
        /// <param name="methodDefinition">Method to that will be injected resolved validation methods</param>
        /// <returns>Method of validation of contract</returns>
        ContractValidate Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition);
    }
}
