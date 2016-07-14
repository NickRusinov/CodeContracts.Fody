using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;

namespace CodeContracts.Fody.ContractCleaners
{
    /// <summary>
    /// No removes custom contract attribute from assembly. Represents NullObject for <see cref="IContractCleaner"/>
    /// </summary>
    public class DisabledContractCleaner : IContractCleaner
    {
        /// <inheritdoc/>
        public void Clean(ContractDefinition contractDefinition)
        {

        }
    }
}
