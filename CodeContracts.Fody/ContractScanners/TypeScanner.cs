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
    public class TypeScanner : ITypeScanner
    {
        private readonly IMethodScanner methodScanner;

        private readonly IPropertyScanner propertyScanner;

        private readonly IFieldScanner fieldScanner;

        public TypeScanner(IMethodScanner methodScanner, IPropertyScanner propertyScanner, IFieldScanner fieldScanner)
        {
            Contract.Requires(methodScanner != null);
            Contract.Requires(propertyScanner != null);
            Contract.Requires(fieldScanner != null);

            this.methodScanner = methodScanner;
            this.propertyScanner = propertyScanner;
            this.fieldScanner = fieldScanner;
        }

        public IEnumerable<ContractDefinition> Scan(TypeDefinition typeDefinition)
        {
            return EnumerableUtils.Concat(
                from methodDefinition in typeDefinition.Methods
                from contractDefinition in methodScanner.Scan(methodDefinition)
                select contractDefinition,

                from propertyDefinition in typeDefinition.Properties
                from contractDefinition in propertyScanner.Scan(propertyDefinition)
                select contractDefinition,
                
                from fieldDefinition in typeDefinition.Fields
                from contractDefinition in fieldScanner.Scan(fieldDefinition)
                select contractDefinition);
        }
    }
}
