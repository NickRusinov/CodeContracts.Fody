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
        private readonly IPropertyGetScanner propertyGetScanner;

        private readonly IPropertySetScanner propertySetScanner;

        public PropertyScanner(IPropertyGetScanner propertyGetScanner, IPropertySetScanner propertySetScanner)
        {
            Contract.Requires(propertyGetScanner != null);
            Contract.Requires(propertySetScanner != null);

            this.propertyGetScanner = propertyGetScanner;
            this.propertySetScanner = propertySetScanner;
        }

        public IEnumerable<ContractDefinition> Scan(PropertyDefinition propertyDefinition)
        {
            return EnumerableUtils.Concat(
                from contractDefinition in propertyGetScanner.Scan(propertyDefinition)
                select contractDefinition,

                from contractDefinition in propertySetScanner.Scan(propertyDefinition)
                select contractDefinition);
        }
    }
}
