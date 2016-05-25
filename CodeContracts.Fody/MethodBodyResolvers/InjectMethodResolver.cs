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
    public class InjectMethodResolver : IInjectMethodResolver
    {
        private readonly IRequiresResolver requiresResolver;

        private readonly IEnsuresResolver ensuresResolver;

        private readonly IInvariantResolver invariantResolver;

        public InjectMethodResolver(IRequiresResolver requiresResolver, IEnsuresResolver ensuresResolver, IInvariantResolver invariantResolver)
        {
            Contract.Requires(requiresResolver != null);
            Contract.Requires(ensuresResolver != null);
            Contract.Requires(invariantResolver != null);

            this.requiresResolver = requiresResolver;
            this.ensuresResolver = ensuresResolver;
            this.invariantResolver = invariantResolver;
        }

        public MethodDefinition Resolve(ContractDefinition contractDefinition)
        {
            var requiresDefinition = contractDefinition as RequiresDefinition;
            if (requiresDefinition != null)
                return requiresResolver.Resolve(requiresDefinition);

            var ensuresDefinition = contractDefinition as EnsuresDefinition;
            if (ensuresDefinition != null)
                return ensuresResolver.Resolve(ensuresDefinition);

            var invariantDefinition = contractDefinition as InvariantDefinition;
            if (invariantDefinition != null)
                return invariantResolver.Resolve(invariantDefinition);

            throw new NotSupportedException();
        }
    }
}
