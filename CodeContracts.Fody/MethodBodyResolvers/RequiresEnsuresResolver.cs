using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.MethodBodyResolvers
{
    public class RequiresEnsuresResolver : IRequiresResolver, IEnsuresResolver
    {
        private readonly IContractClassResolver contractClassResolver;

        public RequiresEnsuresResolver(IContractClassResolver contractClassResolver)
        {
            Contract.Requires(contractClassResolver != null);

            this.contractClassResolver = contractClassResolver;
        }

        public MethodBody Resolve(RequiresDefinition requiresDefinition)
        {
            return Resolve(requiresDefinition, requiresDefinition.ContractMethod);
        }

        public MethodBody Resolve(EnsuresDefinition ensuresDefinition)
        {
            return Resolve(ensuresDefinition, ensuresDefinition.ContractMethod);
        }

        protected MethodBody Resolve(ContractDefinition contractDefinition, MethodDefinition contractMethod)
        {
            return
                (from methodDefinition in contractClassResolver.Resolve(contractDefinition.DeclaringType).Methods
                 where methodDefinition.IsOverride(contractMethod)
                 select methodDefinition.Body)
                .Single();
        }
    }
}
