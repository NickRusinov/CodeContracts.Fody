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
    public class Injector : IInjector
    {
        private readonly IContractValidatesResolver contractValidatesResolver;

        private readonly IInstructionsBuilder instructionsBuilder;

        public Injector(IContractValidatesResolver contractValidatesResolver, IInstructionsBuilder instructionsBuilder)
        {
            Contract.Requires(contractValidatesResolver != null);
            Contract.Requires(instructionsBuilder != null);

            this.contractValidatesResolver = contractValidatesResolver;
            this.instructionsBuilder = instructionsBuilder;
        }

        public void Inject(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            var instructions = contractValidatesResolver.Resolve(contractDefinition, methodDefinition).SelectMany(instructionsBuilder.Build);

            methodDefinition.Body.Instructions.AddRange(instructions);
        }
    }
}
