using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractDefinitions
{
    public class InvariantDefinition : ContractDefinition
    {
        public InvariantDefinition(CustomAttribute contractAttribute, ICustomAttributeProvider attributeProvider, TypeDefinition declaringType) 
            : base(contractAttribute, attributeProvider, declaringType)
        {
            Contract.Requires(contractAttribute != null);
            Contract.Requires(attributeProvider != null);
            Contract.Requires(declaringType != null);
        }
    }
}
