using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;

namespace CodeContracts.Fody.ContractCleaners
{
    /// <summary>
    /// Removes custom contract attribute from assembly
    /// </summary>
    public interface IContractCleaner
    {
        /// <summary>
        /// Removes custom contract attribute from assembly
        /// </summary>
        /// <param name="contractDefinition">Definition of contract that will be removed</param>
        void Clean(ContractDefinition contractDefinition);
    }
}
