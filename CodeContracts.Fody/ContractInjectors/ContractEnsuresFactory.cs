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
        private readonly ModuleDefinition moduleDefinition;

        private readonly ContractConfig contractConfig;

        private readonly IInstructionsBuilder instructionsBuilder;
        
        public ContractEnsuresFactory(ModuleDefinition moduleDefinition, ContractConfig contractConfig, IInstructionsBuilder instructionsBuilder)
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
            if (message != null && contractConfig.Ensures.HasFlag(WithMessages))
                return new ContractMethodWithMessageBuilder(instructionsBuilder, EnsuresWithMessage(moduleDefinition), message);

            return new ContractMethodBuilder(instructionsBuilder, Ensures(moduleDefinition));
        }
    }
}
