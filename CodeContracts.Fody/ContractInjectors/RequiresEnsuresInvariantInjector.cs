using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInstructionsBuilders;
using CodeContracts.Fody.ContractValidateResolvers;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Injects il instructions for requires, ensures or invariant expression in specified method
    /// </summary>
    public class RequiresEnsuresInvariantInjector : IRequiresInjector, IEnsuresInjector, IInvariantInjector
    {
        /// <summary>
        /// Resolves validate method for injecting to specified methods
        /// </summary>
        private readonly IContractValidateResolver contractValidateResolver;

        /// <summary>
        /// Creates il instructions for injected method of contract attribute
        /// </summary>
        private readonly IInstructionsBuilder instructionsBuilder;

        /// <summary>
        /// Initializes a new instance of class <see cref="RequiresEnsuresInvariantInjector"/>
        /// </summary>
        /// <param name="contractValidateResolver">Resolves validate method for injecting to specified methods</param>
        /// <param name="instructionsBuilder">Creates il instructions for injected method of contract attribute</param>
        public RequiresEnsuresInvariantInjector(IContractValidateResolver contractValidateResolver, IInstructionsBuilder instructionsBuilder)
        {
            Contract.Requires(contractValidateResolver != null);
            Contract.Requires(instructionsBuilder != null);

            this.contractValidateResolver = contractValidateResolver;
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
            var instructions = contractValidateResolver.Resolve(contractDefinition, methodDefinition).SelectMany(instructionsBuilder.Build);

            var callBaseCtorInstruction = methodDefinition.Body.Instructions.FirstOrDefault(i => Equals(i.OpCode, OpCodes.Call) && ((MethodReference)i.Operand).Resolve().IsConstructor);
            var callBaseCtorInstructionIndex = methodDefinition.Body.Instructions.IndexOf(callBaseCtorInstruction) + 1;

            methodDefinition.Body.Instructions.InsertRange(callBaseCtorInstructionIndex, instructions);
        }
    }
}
