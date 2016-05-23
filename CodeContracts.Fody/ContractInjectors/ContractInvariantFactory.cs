using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractInvariantFactory : IContractMethodFactory
    {
        private readonly IInstructionsBuilder instructionsBuilder;

        public ContractInvariantFactory(IInstructionsBuilder instructionsBuilder)
        {
            Contract.Requires(instructionsBuilder != null);

            this.instructionsBuilder = instructionsBuilder;
        }

        public IInstructionsBuilder Create(ModuleDefinition moduleDefinition, TypeDefinition typeDefinition, string message)
        {
            if (message != null)
                return new ContractMethodWithMessageBuilder(instructionsBuilder, ContractReferences.InvariantWithMessage(moduleDefinition), message);

            return new ContractMethodBuilder(instructionsBuilder, ContractReferences.Invariant(moduleDefinition));
        }
    }
}
