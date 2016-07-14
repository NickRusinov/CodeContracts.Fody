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
    public class ContractCleaner : IContractCleaner
    {
        /// <inheritdoc/>
        public void Clean(ContractDefinition contractDefinition)
        {
            contractDefinition.AttributeProvider.CustomAttributes.Remove(contractDefinition.ContractAttribute);
        }
    }
}
