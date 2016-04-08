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
    public class MethodReturnScanner : IMethodReturnScanner
    {
        private readonly IContractCriteria contractCriteria;

        public MethodReturnScanner(IContractCriteria contractCriteria)
        {
            Contract.Requires(contractCriteria != null);

            this.contractCriteria = contractCriteria;
        }

        public IEnumerable<ContractDefinition> Scan(MethodReturnType methodReturnDefinition)
        {
            return
                from contractAttribute in methodReturnDefinition.CustomAttributes
                where contractCriteria.IsContract(contractAttribute)
                let methodDefinition = (MethodDefinition)methodReturnDefinition.Method
                select new EnsuresDefinition(contractAttribute, methodDefinition.DeclaringType, methodDefinition);
        }
    }
}
