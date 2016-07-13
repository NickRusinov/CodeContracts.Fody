using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractValidateResolvers
{
    /// <summary>
    /// Scans validate methods in a custom attribute
    /// </summary>
    public interface IContractValidateScanner
    {
        /// <summary>
        /// Scans validate methods in a custom attribute
        /// </summary>
        /// <param name="customAttribute">Custom attribute that will be scan</param>
        /// <returns>Collection of all found validate methods</returns>
        IEnumerable<ContractValidateDefinition> Scan(CustomAttribute customAttribute);
    }
}
