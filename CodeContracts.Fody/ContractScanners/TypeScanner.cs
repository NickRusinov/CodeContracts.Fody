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
    /// Scans custom contract attributes in a type
    /// </summary>
    public class TypeScanner : ITypeScanner
    {
        /// <summary>
        /// Scans custom contract attributes in a method
        /// </summary>
        private readonly IMethodScanner methodScanner;

        /// <summary>
        /// Scans custom contract attributes in a property
        /// </summary>
        private readonly IPropertyScanner propertyScanner;

        /// <summary>
        /// Scans custom contract attributes in a field
        /// </summary>
        private readonly IFieldScanner fieldScanner;

        /// <summary>
        /// Initializes a new instance of class <see cref="TypeScanner"/>
        /// </summary>
        /// <param name="methodScanner">Scans custom contract attributes in a method</param>
        /// <param name="propertyScanner">Scans custom contract attributes in a property</param>
        /// <param name="fieldScanner">Scans custom contract attributes in a field</param>
        public TypeScanner(IMethodScanner methodScanner, IPropertyScanner propertyScanner, IFieldScanner fieldScanner)
        {
            Contract.Requires(methodScanner != null);
            Contract.Requires(propertyScanner != null);
            Contract.Requires(fieldScanner != null);

            this.methodScanner = methodScanner;
            this.propertyScanner = propertyScanner;
            this.fieldScanner = fieldScanner;
        }

        /// <inheritdoc/>
        public IEnumerable<ContractDefinition> Scan(TypeDefinition typeDefinition)
        {
            return EnumerableExtensions.Concat(
                from methodDefinition in typeDefinition.Methods
                where !methodDefinition.IsSetter && !methodDefinition.IsGetter
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
