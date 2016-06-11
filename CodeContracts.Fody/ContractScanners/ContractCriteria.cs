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
    public class ContractCriteria : IContractCriteria
    {
        public bool IsContract(CustomAttribute attribute)
        {
            var contractAttributeType = attribute.AttributeType.Module.ImportReference(typeof(ContractAttribute));

            return attribute.AttributeType.GetBaseTypes().Contains(contractAttributeType, TypeReferenceComparer.Instance);
        }
    }
}
