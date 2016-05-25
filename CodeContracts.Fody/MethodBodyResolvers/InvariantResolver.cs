using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.MethodBodyResolvers
{
    public class InvariantResolver : IInvariantResolver
    {
        private readonly IContractClassResolver contractClassResolver;

        private readonly IInvariantMethodResolver invariantMethodResolver;

        public InvariantResolver(IContractClassResolver contractClassResolver, IInvariantMethodResolver invariantMethodResolver)
        {
            Contract.Requires(contractClassResolver != null);
            Contract.Requires(invariantMethodResolver != null);

            this.contractClassResolver = contractClassResolver;
            this.invariantMethodResolver = invariantMethodResolver;
        }

        public MethodDefinition Resolve(InvariantDefinition invariantDefinition)
        {
            return invariantMethodResolver.Resolve(contractClassResolver.Resolve(invariantDefinition.DeclaringType, null));
        }
    }
}
