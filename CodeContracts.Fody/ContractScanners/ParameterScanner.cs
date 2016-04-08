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
    public class ParameterScanner : IParameterScanner
    {
        private readonly IContractCriteria contractCriteria;

        public ParameterScanner(IContractCriteria contractCriteria)
        {
            Contract.Requires(contractCriteria != null);

            this.contractCriteria = contractCriteria;
        }

        public IEnumerable<ContractDefinition> Scan(ParameterDefinition parameterDefinition)
        {
            return
                from contractAttribute in parameterDefinition.CustomAttributes
                where contractCriteria.IsContract(contractAttribute)
                let methodDefinition = (MethodDefinition)parameterDefinition.Method
                select new RequiresDefinition(contractAttribute, methodDefinition.DeclaringType, methodDefinition);
        }
    }
}
