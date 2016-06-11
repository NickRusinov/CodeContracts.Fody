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
    /// Scans custom contract attributes in a field
    /// </summary>
    public class FieldScanner : IFieldScanner
    {
        /// <summary>
        /// Criteria that define that custom attribute is contract attribute
        /// </summary>
        private readonly IContractCriteria contractCriteria;

        /// <summary>
        /// Initializes a new instance of class <see cref="FieldScanner"/>
        /// </summary>
        /// <param name="contractCriteria">Criteria that define that custom attribute is contract attribute</param>
        public FieldScanner(IContractCriteria contractCriteria)
        {
            Contract.Requires(contractCriteria != null);

            this.contractCriteria = contractCriteria;
        }

        /// <inheritdoc/>
        public IEnumerable<ContractDefinition> Scan(FieldDefinition fieldDefinition)
        {
            return from contractAttribute in fieldDefinition.CustomAttributes
                   where contractCriteria.IsContract(contractAttribute)
                   select new InvariantDefinition(contractAttribute, fieldDefinition, fieldDefinition.DeclaringType);
        }
    }
}
