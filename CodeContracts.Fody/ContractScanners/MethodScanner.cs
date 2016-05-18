using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractScanners
{
    public class MethodScanner : IMethodScanner
    {
        private readonly IParameterScanner parameterScanner;

        private readonly IMethodReturnScanner methodReturnScanner;

        private readonly IContractCriteria contractCriteria;

        public MethodScanner(IParameterScanner parameterScanner, IMethodReturnScanner methodReturnScanner, IContractCriteria contractCriteria)
        {
            Contract.Requires(parameterScanner != null);
            Contract.Requires(methodReturnScanner != null);
            Contract.Requires(contractCriteria != null);

            this.parameterScanner = parameterScanner;
            this.methodReturnScanner = methodReturnScanner;
            this.contractCriteria = contractCriteria;
        }

        public IEnumerable<ContractDefinition> Scan(MethodDefinition methodDefinition)
        {
            return EnumerableUtils.Concat(
                from parameterDefinition in methodDefinition.Parameters
                from contractDefinition in parameterScanner.Scan(parameterDefinition)
                select contractDefinition,

                from methodReturnDefinition in Enumerable.Repeat(methodDefinition.MethodReturnType, 1)
                from contractDefinition in methodReturnScanner.Scan(methodReturnDefinition)
                select contractDefinition,
                
                from contractAttribute in methodDefinition.CustomAttributes
                where contractCriteria.IsContract(contractAttribute)
                select new RequiresDefinition(contractAttribute, methodDefinition, methodDefinition));
        }
    }
}
