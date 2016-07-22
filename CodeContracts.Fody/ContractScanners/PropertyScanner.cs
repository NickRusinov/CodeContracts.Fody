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
    /// <summary>
    /// Scans custom contract attributes in a property
    /// </summary>
    public class PropertyScanner : IPropertyScanner
    {
        /// <summary>
        /// Criteria that define that custom attribute is contract attribute
        /// </summary>
        private readonly IContractCriteria contractCriteria;

        /// <summary>
        /// Initializes a new instance of class <see cref="PropertyScanner"/>
        /// </summary>
        /// <param name="contractCriteria">Criteria that define that custom attribute is contract attribute</param>
        public PropertyScanner(IContractCriteria contractCriteria)
        {
            Contract.Requires(contractCriteria != null);

            this.contractCriteria = contractCriteria;
        }

        /// <inheritdoc/>
        public IEnumerable<ContractDefinition> Scan(PropertyDefinition propertyDefinition)
        {
            return EnumerableExtensions.Concat<ContractDefinition>(
                from contractAttribute in propertyDefinition.CustomAttributes
                where contractCriteria.IsContract(contractAttribute)
                where propertyDefinition.SetMethod != null
                select new RequiresDefinition(contractAttribute, propertyDefinition, propertyDefinition.SetMethod),

                from contractAttribute in propertyDefinition.CustomAttributes
                where contractCriteria.IsContract(contractAttribute)
                where propertyDefinition.GetMethod != null
                select new EnsuresDefinition(contractAttribute, propertyDefinition, propertyDefinition.GetMethod));
        }
    }
}
