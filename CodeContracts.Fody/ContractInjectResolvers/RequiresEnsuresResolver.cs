using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectResolvers
{
    public class RequiresEnsuresResolver : IRequiresResolver, IEnsuresResolver
    {
        private readonly IContractClassResolver contractClassResolver;

        public RequiresEnsuresResolver(IContractClassResolver contractClassResolver)
        {
            Contract.Requires(contractClassResolver != null);

            this.contractClassResolver = contractClassResolver;
        }

        public MethodDefinition Resolve(RequiresDefinition requiresDefinition)
        {
            return Resolve(requiresDefinition, requiresDefinition.ContractMethod);
        }

        public MethodDefinition Resolve(EnsuresDefinition ensuresDefinition)
        {
            return Resolve(ensuresDefinition, ensuresDefinition.ContractMethod);
        }

        protected MethodDefinition Resolve(ContractDefinition contractDefinition, MethodDefinition contractMethod)
        {
            return (from methodDefinition in contractClassResolver.Resolve(contractDefinition.DeclaringType, contractMethod).Methods
                    where methodDefinition.IsOverride(contractMethod)
                    select methodDefinition)
                .Single();
        }
    }
}
