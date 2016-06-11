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
    /// Scans custom contract attributes in a parameter of method
    /// </summary>
    public class ParameterScanner : IParameterScanner
    {
        /// <summary>
        /// Criteria that define that custom attribute is contract attribute
        /// </summary>
        private readonly IContractCriteria contractCriteria;

        /// <summary>
        /// Initializes a new instance of class <see cref="ParameterScanner"/>
        /// </summary>
        /// <param name="contractCriteria">Criteria that define that custom attribute is contract attribute</param>
        public ParameterScanner(IContractCriteria contractCriteria)
        {
            Contract.Requires(contractCriteria != null);

            this.contractCriteria = contractCriteria;
        }

        /// <inheritdoc/>
        public IEnumerable<ContractDefinition> Scan(ParameterDefinition parameterDefinition)
        {
            return from contractAttribute in parameterDefinition.CustomAttributes
                   where contractCriteria.IsContract(contractAttribute)
                   let methodDefinition = (MethodDefinition)parameterDefinition.Method
                   select new RequiresDefinition(contractAttribute, parameterDefinition, methodDefinition);
        }
    }
}
