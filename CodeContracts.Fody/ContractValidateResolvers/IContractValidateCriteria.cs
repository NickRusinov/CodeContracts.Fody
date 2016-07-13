using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractValidateResolvers
{
    /// <summary>
    /// Criteria that defines that specified method is contract validate method
    /// </summary>
    public interface IContractValidateCriteria
    {
        /// <summary>
        /// Defines that specified method is contract validate method
        /// </summary>
        /// <param name="methodDefinition">Method that will be checked</param>
        /// <returns>True if specified method is validate method; false otherwise</returns>
        bool IsContractValidate(MethodDefinition methodDefinition);
    }
}
