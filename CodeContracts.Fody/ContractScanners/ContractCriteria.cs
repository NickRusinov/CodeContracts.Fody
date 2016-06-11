using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractScanners
{
    /// <summary>
    /// Criteria that define that custom attribute is contract attribute
    /// </summary>
    public class ContractCriteria : IContractCriteria
    {
        /// <summary>
        /// Definition of current weaving assembly
        /// </summary>
        private readonly ModuleDefinition moduleDefinition;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractCriteria"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        public ContractCriteria(ModuleDefinition moduleDefinition)
        {
            Contract.Requires(moduleDefinition != null);

            this.moduleDefinition = moduleDefinition;
        }

        /// <inheritdoc/>
        public bool IsContract(CustomAttribute attribute)
        {
            var contractAttributeType = moduleDefinition.ImportReference(typeof(ContractAttribute));

            return attribute.AttributeType.GetBaseTypes().Contains(contractAttributeType, TypeReferenceComparer.Instance);
        }
    }
}
