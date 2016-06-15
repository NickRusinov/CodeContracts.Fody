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
    /// <summary>
    /// Resolves method in which will be injected requries or ensures expressions
    /// </summary>
    public class RequiresEnsuresResolver : IRequiresResolver, IEnsuresResolver
    {
        /// <summary>
        /// Resolves type which is a contract class for specified type
        /// </summary>
        private readonly IContractClassResolver contractClassResolver;

        /// <summary>
        /// Initializes a new instance of class <see cref="RequiresEnsuresResolver"/>
        /// </summary>
        /// <param name="contractClassResolver">Resolves type which is a contract class for specified type</param>
        public RequiresEnsuresResolver(IContractClassResolver contractClassResolver)
        {
            Contract.Requires(contractClassResolver != null);

            this.contractClassResolver = contractClassResolver;
        }

        /// <inheritdoc/>
        public MethodDefinition Resolve(RequiresDefinition requiresDefinition)
        {
            return Resolve(requiresDefinition, requiresDefinition.ContractMethod);
        }

        /// <inheritdoc/>
        public MethodDefinition Resolve(EnsuresDefinition ensuresDefinition)
        {
            return Resolve(ensuresDefinition, ensuresDefinition.ContractMethod);
        }

        /// <summary>
        /// Resolves method in which will be injected requires or ensures expressions
        /// </summary>
        /// <param name="contractDefinition">Requires or ensures definition for which resolves method</param>
        /// <param name="contractMethod">Method that references requries or ensures definition</param>
        /// <returns>Method in which will be injected requires or ensures expressions</returns>
        private MethodDefinition Resolve(ContractDefinition contractDefinition, MethodDefinition contractMethod)
        {
            return contractClassResolver.Resolve(contractDefinition.DeclaringType, contractMethod).Methods.Single(md => md.IsOverride(contractMethod));
        }
    }
}
