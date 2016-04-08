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
        protected ContractDefinition(CustomAttribute contractAttribute, TypeDefinition declaringType)
        {
            Contract.Requires(contractAttribute != null);
            Contract.Requires(declaringType != null);

            ContractAttribute = contractAttribute;
            DeclaringType = declaringType;
        }

        public CustomAttribute ContractAttribute { get; }

        public TypeDefinition DeclaringType { get; }
    }
}
