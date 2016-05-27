using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using static CodeContracts.Fody.ContractReferences;
using static CodeContracts.Fody.EnsuresMode;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractEnsuresFactory : IContractMethodFactory
    {
        private readonly IInstructionsBuilder instructionsBuilder;

        private readonly ContractConfig contractConfig;

        public ContractEnsuresFactory(IInstructionsBuilder instructionsBuilder, ContractConfig contractConfig)
        {
            Contract.Requires(instructionsBuilder != null);
            Contract.Requires(contractConfig != null);

            this.instructionsBuilder = instructionsBuilder;
            this.contractConfig = contractConfig;
        }

        public IInstructionsBuilder Create(ModuleDefinition moduleDefinition, TypeDefinition typeDefinition, string message)
        {
            if (message != null && contractConfig.Ensures.HasFlag(WithMessages))
                return new ContractMethodWithMessageBuilder(instructionsBuilder, EnsuresWithMessage(moduleDefinition), message);

            return new ContractMethodBuilder(instructionsBuilder, Ensures(moduleDefinition));
        }
    }
}
