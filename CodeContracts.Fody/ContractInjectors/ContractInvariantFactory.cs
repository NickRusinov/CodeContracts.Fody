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
        private readonly ModuleDefinition moduleDefinition;

        private readonly ContractConfig contractConfig;

        private readonly IInstructionsBuilder instructionsBuilder;
        
        public ContractInvariantFactory(ModuleDefinition moduleDefinition, ContractConfig contractConfig, IInstructionsBuilder instructionsBuilder)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(contractConfig != null);
            Contract.Requires(instructionsBuilder != null);

            this.moduleDefinition = moduleDefinition;
            this.contractConfig = contractConfig;
            this.instructionsBuilder = instructionsBuilder;
        }

        public IInstructionsBuilder Create(TypeDefinition typeDefinition, string message)
        {
            if (message != null && contractConfig.Invariant.HasFlag(WithMessages))
                return new ContractMethodWithMessageBuilder(instructionsBuilder, InvariantWithMessage(moduleDefinition), message);

            return new ContractMethodBuilder(instructionsBuilder, Invariant(moduleDefinition));
        }
    }
}
