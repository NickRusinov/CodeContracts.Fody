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
    public class ContractInjector : IRequiresInjector, IEnsuresInjector, IInvariantInjector
    {
        private readonly IContractValidatesResolver contractValidatesResolver;

        private readonly IInstructionsBuilder instructionsBuilder;

        public ContractInjector(IContractValidatesResolver contractValidatesResolver, IInstructionsBuilder instructionsBuilder)
        {
            Contract.Requires(contractValidatesResolver != null);
            Contract.Requires(instructionsBuilder != null);

            this.contractValidatesResolver = contractValidatesResolver;
            this.instructionsBuilder = instructionsBuilder;
        }

        public void Inject(RequiresDefinition requiresDefinition, MethodDefinition methodDefinition)
        {
            Inject(requiresDefinition as ContractDefinition, methodDefinition);
        }

        public void Inject(EnsuresDefinition ensuresDefinition, MethodDefinition methodDefinition)
        {
            Inject(ensuresDefinition as ContractDefinition, methodDefinition);
        }

        public void Inject(InvariantDefinition invariantDefinition, MethodDefinition methodDefinition)
        {
            Inject(invariantDefinition as ContractDefinition, methodDefinition);
        }

        public void Inject(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            var instructions = contractValidatesResolver.Resolve(contractDefinition, methodDefinition).SelectMany(instructionsBuilder.Build);

            methodDefinition.Body.Instructions.AddRange(instructions);
        }
    }
}
