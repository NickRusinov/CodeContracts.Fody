using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractScanners
{
    /// <summary>
    /// Criteria that define that custom attribute is contract attribute
    /// </summary>
    public class ContractCriteria : IContractCriteria
    {
        /// <inheritdoc/>
        public bool IsContract(CustomAttribute attribute)
        {
            return attribute.AttributeType.GetBaseTypes()
                .Any(tr => tr.Name == "ContractAttribute");
        }
    }
}
