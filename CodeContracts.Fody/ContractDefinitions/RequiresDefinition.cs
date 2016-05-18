using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractDefinitions
{
    public class RequiresDefinition : ContractDefinition
    {
        public RequiresDefinition(CustomAttribute contractAttribute, ICustomAttributeProvider attributeProvider, MethodDefinition contractMethod)
            : base(contractAttribute, attributeProvider, contractMethod.DeclaringType)
        {
            Contract.Requires(contractAttribute != null);
            Contract.Requires(attributeProvider != null);
            Contract.Requires(contractMethod != null);

            ContractMethod = contractMethod;
        }

        public MethodDefinition ContractMethod { get; set; }
    }
}
