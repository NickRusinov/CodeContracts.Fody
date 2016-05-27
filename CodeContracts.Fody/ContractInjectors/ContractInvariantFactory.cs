using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using static CodeContracts.Fody.ContractReferences;
using static CodeContracts.Fody.InvariantMode;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractInvariantFactory : IContractMethodFactory
    {
        private readonly IInstructionsBuilder instructionsBuilder;

        private readonly ContractConfig contractConfig;

        public ContractInvariantFactory(IInstructionsBuilder instructionsBuilder, ContractConfig contractConfig)
        {
            Contract.Requires(instructionsBuilder != null);
            Contract.Requires(contractConfig != null);

            this.instructionsBuilder = instructionsBuilder;
            this.contractConfig = contractConfig;
        }

        public IInstructionsBuilder Create(ModuleDefinition moduleDefinition, TypeDefinition typeDefinition, string message)
        {
            if (message != null && contractConfig.Invariant.HasFlag(WithMessages))
                return new ContractMethodWithMessageBuilder(instructionsBuilder, InvariantWithMessage(moduleDefinition), message);

            return new ContractMethodBuilder(instructionsBuilder, Invariant(moduleDefinition));
        }
    }
}
