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
    /// <summary>
    /// Scans custom contract attributes in a method return value
    /// </summary>
    public class MethodReturnScanner : IMethodReturnScanner
    {
        /// <summary>
        /// Criteria that define that custom attribute is contract attribute
        /// </summary>
        private readonly IContractCriteria contractCriteria;

        /// <summary>
        /// Initializes a new instance of class <see cref="MethodReturnScanner"/>
        /// </summary>
        /// <param name="contractCriteria">Criteria that define that custom attribute is contract attribute</param>
        public MethodReturnScanner(IContractCriteria contractCriteria)
        {
            Contract.Requires(contractCriteria != null);

            this.contractCriteria = contractCriteria;
        }

        /// <inheritdoc/>
        public IEnumerable<ContractDefinition> Scan(MethodReturnType methodReturnDefinition)
        {
            return from contractAttribute in methodReturnDefinition.CustomAttributes
                   where contractCriteria.IsContract(contractAttribute)
                   let methodDefinition = (MethodDefinition)methodReturnDefinition.Method
                   select new EnsuresDefinition(contractAttribute, methodReturnDefinition, methodDefinition);
        }
    }
}
