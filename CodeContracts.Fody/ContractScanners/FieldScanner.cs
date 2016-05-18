using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractScanners
{
    public class FieldScanner : IFieldScanner
    {
        private readonly IContractCriteria contractCriteria;

        public FieldScanner(IContractCriteria contractCriteria)
        {
            Contract.Requires(contractCriteria != null);

            this.contractCriteria = contractCriteria;
        }

        public IEnumerable<ContractDefinition> Scan(FieldDefinition fieldDefinition)
        {
            return
                from contractAttribute in fieldDefinition.CustomAttributes
                where contractCriteria.IsContract(contractAttribute)
                select new InvariantDefinition(contractAttribute, fieldDefinition, fieldDefinition.DeclaringType);
        }
    }
}
