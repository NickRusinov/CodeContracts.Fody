using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractDefinitions
{
    public abstract class ContractDefinition
    {
        protected ContractDefinition(CustomAttribute contractAttribute, ICustomAttributeProvider attributeProvider, TypeDefinition declaringType)
        {
            Contract.Requires(contractAttribute != null);
            Contract.Requires(attributeProvider != null);
            Contract.Requires(declaringType != null);

            ContractAttribute = contractAttribute;
            AttributeProvider = attributeProvider;
            DeclaringType = declaringType;
        }

        public CustomAttribute ContractAttribute { get; }

        public ICustomAttributeProvider AttributeProvider { get; }

        public TypeDefinition DeclaringType { get; }
    }
}
