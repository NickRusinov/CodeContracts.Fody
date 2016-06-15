using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectResolvers
{
    /// <summary>
    /// Resolves method in which will be injected contract (requires, ensures or invariant) expressions
    /// </summary>
    public class ContractInjectResolver : IContractInjectResolver
    {
        /// <summary>
        /// Resolves method in which will be injected requires expressions
        /// </summary>
        private readonly IRequiresResolver requiresResolver;

        /// <summary>
        /// Resolves method in which will be injected ensures expressions
        /// </summary>
        private readonly IEnsuresResolver ensuresResolver;

        /// <summary>
        /// Resolves method in which will be injected invariant expressions
        /// </summary>
        private readonly IInvariantResolver invariantResolver;

        /// <summary>
        /// Initailizes a new instance of class <see cref="ContractInjectResolver"/>
        /// </summary>
        /// <param name="requiresResolver">Resolves method in which will be injected requires expressions</param>
        /// <param name="ensuresResolver">Resolves method in which will be injected ensures expressions</param>
        /// <param name="invariantResolver">Resolves method in which will be injected invariant expressions</param>
        public ContractInjectResolver(IRequiresResolver requiresResolver, IEnsuresResolver ensuresResolver, IInvariantResolver invariantResolver)
        {
            Contract.Requires(requiresResolver != null);
            Contract.Requires(ensuresResolver != null);
            Contract.Requires(invariantResolver != null);

            this.requiresResolver = requiresResolver;
            this.ensuresResolver = ensuresResolver;
            this.invariantResolver = invariantResolver;
        }

        /// <inheritdoc/>
        /// <exception cref="NotSupportedException">
        /// Throws if contract definition isn't requires, ensures or invariant definition 
        /// (other definitions are not allowed)
        /// </exception>
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
