using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractValidateScanner : IContractValidateScanner
    {
        private readonly IContractValidateCriteria contractValidateCriteria;

        public ContractValidateScanner(IContractValidateCriteria contractValidateCriteria)
        {
            Contract.Requires(contractValidateCriteria != null);

            this.contractValidateCriteria = contractValidateCriteria;
        }

        public IEnumerable<ContractValidateDefinition> Scan(ContractDefinition contractDefinition)
        {
            // todo - алгоритм сканирования атрибута контракта для нахождения методов валидации
            return Enumerable.Empty<ContractValidateDefinition>();
        }
    }
}
