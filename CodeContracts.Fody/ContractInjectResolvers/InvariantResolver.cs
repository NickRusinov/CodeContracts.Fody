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
    /// Resolves method in which will be injected invariant expressions
    /// </summary>
    public class InvariantResolver : IInvariantResolver
    {
        /// <summary>
        /// Resolves type which is a contract class for specified type
        /// </summary>
        private readonly IContractClassResolver contractClassResolver;

        /// <summary>
        /// Resolves method which is a invariant method for specified type
        /// </summary>
        private readonly IInvariantMethodResolver invariantMethodResolver;

        /// <summary>
        /// Initializes a new instance of class <see cref="InvariantResolver"/>
        /// </summary>
        /// <param name="contractClassResolver">Resolves type which is a contract class for specified type</param>
        /// <param name="invariantMethodResolver">Resolves method which is a invariant method for specified type</param>
        public InvariantResolver(IContractClassResolver contractClassResolver, IInvariantMethodResolver invariantMethodResolver)
        {
            Contract.Requires(contractClassResolver != null);
            Contract.Requires(invariantMethodResolver != null);

            this.contractClassResolver = contractClassResolver;
            this.invariantMethodResolver = invariantMethodResolver;
        }

        /// <inheritdoc/>
        public MethodDefinition Resolve(InvariantDefinition invariantDefinition)
        {
            return invariantMethodResolver.Resolve(contractClassResolver.Resolve(invariantDefinition.DeclaringType, null));
        }
    }
}
