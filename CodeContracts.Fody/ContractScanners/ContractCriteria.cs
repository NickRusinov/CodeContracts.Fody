using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractScanners
{
    public class ContractCriteria : IContractCriteria
    {
        public bool IsContract(CustomAttribute attribute)
        {
            // todo - алгоритм распознавания атрибута контракта
            return true;
        }
    }
}
