using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractValidateCriteria : IContractValidateCriteria
    {
        public bool IsContractValidate(MethodDefinition methodDefinition)
        {
            // todo - алгоритм распознавания метода валидации
            return true;
        }
    }
}
