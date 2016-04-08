using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractDefinitions
{
    public class EnsuresDefinition : ContractDefinition
    {
        public EnsuresDefinition(CustomAttribute contractAttribute, TypeDefinition declaringType, MethodDefinition contractMethod)
            : base(contractAttribute, declaringType)
        {
            Contract.Requires(contractAttribute != null);
            Contract.Requires(declaringType != null);
            Contract.Requires(contractMethod != null);

            ContractMethod = contractMethod;
        }

        public MethodDefinition ContractMethod { get; set; }
    }
}
