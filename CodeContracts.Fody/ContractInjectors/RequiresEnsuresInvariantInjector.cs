using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Injects il instructions for requires, ensures or invariant expression in specified method
    /// </summary>
    public class RequiresEnsuresInvariantInjector : IRequiresInjector, IEnsuresInjector, IInvariantInjector
    {
        /// <summary>
        /// Resolves collection of validate methods for injecting to specified method
        /// </summary>
        private readonly IContractValidatesResolver contractValidatesResolver;

        /// <summary>
        /// Creates il instructions for injected method of contract attribute
        /// </summary>
        private readonly IInstructionsBuilder instructionsBuilder;

        /// <summary>
        /// Initializes a new instance of class <see cref="RequiresEnsuresInvariantInjector"/>
        /// </summary>
        /// <param name="contractValidatesResolver">Resolves collection of validate methods for injecting to specified method</param>
        /// <param name="instructionsBuilder">Creates il instructions for injected method of contract attribute</param>
        public RequiresEnsuresInvariantInjector(IContractValidatesResolver contractValidatesResolver, IInstructionsBuilder instructionsBuilder)
        {
            Contract.Requires(contractValidatesResolver != null);
            Contract.Requires(instructionsBuilder != null);

            this.contractValidatesResolver = contractValidatesResolver;
            this.instructionsBuilder = instructionsBuilder;
        }

        /// <inheritdoc/>
        public void Inject(RequiresDefinition requiresDefinition, MethodDefinition methodDefinition)
        {
            Inject(requiresDefinition as ContractDefinition, methodDefinition);
        }

        /// <inheritdoc/>
        public void Inject(EnsuresDefinition ensuresDefinition, MethodDefinition methodDefinition)
        {
            Inject(ensuresDefinition as ContractDefinition, methodDefinition);
        }

        /// <inheritdoc/>
        public void Inject(InvariantDefinition invariantDefinition, MethodDefinition methodDefinition)
        {
            Inject(invariantDefinition as ContractDefinition, methodDefinition);
        }

        /// <summary>
        /// Injects il instructions for requries, ensures or invariant expression in specified method
        /// </summary>
        /// <param name="contractDefinition">Requires, ensures or invariant definition for injecting to method</param>
        /// <param name="methodDefinition">Method in that will be injected il instructions</param>
        public void Inject(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            var instructions = contractValidatesResolver.Resolve(contractDefinition, methodDefinition).SelectMany(instructionsBuilder.Build);

            methodDefinition.Body.Instructions.AddRange(instructions);
        }
    }
}
