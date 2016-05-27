using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using static CodeContracts.Fody.ContractReferences;
using static CodeContracts.Fody.RequiresMode;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractRequiresFactory : IContractMethodFactory
    {
        private readonly ModuleDefinition moduleDefinition;

        private readonly ContractConfig contractConfig;

        private readonly IInstructionsBuilder instructionsBuilder;
        
        public ContractRequiresFactory(ModuleDefinition moduleDefinition, ContractConfig contractConfig, IInstructionsBuilder instructionsBuilder)
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
            if (typeDefinition != null && message != null && contractConfig.Requires.HasFlag(WithMessages | WithExceptions))
                return new ContractMethodWithMessageBuilder(instructionsBuilder, RequiresWithExceptionAndMessage(moduleDefinition, typeDefinition), message);

            if (typeDefinition != null && contractConfig.Requires.HasFlag(WithExceptions))
                return new ContractMethodBuilder(instructionsBuilder, RequiresWithException(moduleDefinition, typeDefinition));

            if (message != null && contractConfig.Requires.HasFlag(WithMessages))
                return new ContractMethodWithMessageBuilder(instructionsBuilder, RequiresWithMessage(moduleDefinition), message);

            return new ContractMethodBuilder(instructionsBuilder, Requires(moduleDefinition));
        }
    }
}
