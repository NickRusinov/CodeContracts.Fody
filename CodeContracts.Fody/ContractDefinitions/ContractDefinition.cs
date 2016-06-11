using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractDefinitions
{
    /// <summary>
    /// Base class for all contract definitions (requires, ensures or invariant)
    /// </summary>
    public abstract class ContractDefinition
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="ContractDefinition"/>
        /// </summary>
        /// <param name="contractAttribute">Custom attribute that contains information about contract</param>
        /// <param name="attributeProvider">Member to which was applied custom attribute</param>
        /// <param name="declaringType">Type that contains <paramref name="attributeProvider"/></param>
        protected ContractDefinition(CustomAttribute contractAttribute, ICustomAttributeProvider attributeProvider, TypeDefinition declaringType)
        {
            Contract.Requires(contractAttribute != null);
            Contract.Requires(attributeProvider != null);
            Contract.Requires(declaringType != null);

            ContractAttribute = contractAttribute;
            AttributeProvider = attributeProvider;
            DeclaringType = declaringType;
        }

        /// <summary>
        /// Custom attribute that contains information about contract
        /// </summary>
        public CustomAttribute ContractAttribute { get; }

        /// <summary>
        /// Member to which was applied custom attribute
        /// </summary>
        public ICustomAttributeProvider AttributeProvider { get; }

        /// <summary>
        /// Type that contains <see cref="AttributeProvider"/>
        /// </summary>
        public TypeDefinition DeclaringType { get; }
    }
}
