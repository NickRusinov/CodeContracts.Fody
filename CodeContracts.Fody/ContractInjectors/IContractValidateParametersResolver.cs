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
    /// Resolves a collection of parameters which can be used for injecting to validation method
    /// </summary>
    public interface IContractValidateParametersResolver
    {
        /// <summary>
        /// Resolves a collection of parameters which can be used for injecting to validation method
        /// </summary>
        /// <param name="contractDefinition">Contract definition for resolves collection of parameters</param>
        /// <param name="methodDefinition">Method in which will be injecting validation method</param>
        /// <returns>Collection of parameters of validation method</returns>
        IEnumerable<ContractValidateParameter> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition);
    }
}
