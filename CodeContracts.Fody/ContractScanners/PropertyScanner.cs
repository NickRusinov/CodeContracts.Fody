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
    public class PropertyScanner : IPropertyScanner
    {
        private readonly IContractCriteria contractCriteria;

        public PropertyScanner(IContractCriteria contractCriteria)
        {
            Contract.Requires(contractCriteria != null);

            this.contractCriteria = contractCriteria;
        }

        public IEnumerable<ContractDefinition> Scan(PropertyDefinition propertyDefinition)
        {
            return EnumerableUtils.Concat<ContractDefinition>(
                from contractAttribute in propertyDefinition.CustomAttributes
                where contractCriteria.IsContract(contractAttribute)
                where propertyDefinition.SetMethod != null
                select new RequiresDefinition(contractAttribute, propertyDefinition, propertyDefinition.SetMethod),

                from contractAttribute in propertyDefinition.CustomAttributes
                where contractCriteria.IsContract(contractAttribute)
                where propertyDefinition.GetMethod != null
                select new EnsuresDefinition(contractAttribute, propertyDefinition, propertyDefinition.GetMethod),

                from contractAttribute in propertyDefinition.CustomAttributes
                where contractCriteria.IsContract(contractAttribute)
                where propertyDefinition.GetMethod != null
                select new InvariantDefinition(contractAttribute, propertyDefinition, propertyDefinition.DeclaringType));
        }
    }
}
