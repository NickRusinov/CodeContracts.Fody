using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractEnsuresFactory : IContractMethodFactory
    {
        private readonly IInstructionsBuilder instructionsBuilder;

        public ContractEnsuresFactory(IInstructionsBuilder instructionsBuilder)
        {
            Contract.Requires(instructionsBuilder != null);

            this.instructionsBuilder = instructionsBuilder;
        }

        public IInstructionsBuilder Create(ModuleDefinition moduleDefinition, TypeDefinition typeDefinition, string message)
        {
            if (message != null)
                return new ContractMethodWithMessageBuilder(instructionsBuilder, ContractReferences.EnsuresWithMessage(moduleDefinition), message);

            return new ContractMethodBuilder(instructionsBuilder, ContractReferences.Ensures(moduleDefinition));
        }
    }
}
