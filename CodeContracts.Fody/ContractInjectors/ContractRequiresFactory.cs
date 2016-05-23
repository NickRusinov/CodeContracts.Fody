using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractRequiresFactory : IContractMethodFactory
    {
        private readonly IInstructionsBuilder instructionsBuilder;

        public ContractRequiresFactory(IInstructionsBuilder instructionsBuilder)
        {
            Contract.Requires(instructionsBuilder != null);

            this.instructionsBuilder = instructionsBuilder;
        }

        public IInstructionsBuilder Create(ModuleDefinition moduleDefinition, TypeDefinition typeDefinition, string message)
        {
            if (typeDefinition != null && message != null)
                return new ContractMethodWithMessageBuilder(instructionsBuilder, ContractReferences.RequiresWithExceptionAndMessage(moduleDefinition, typeDefinition), message);

            if (typeDefinition != null)
                return new ContractMethodBuilder(instructionsBuilder, ContractReferences.RequiresWithException(moduleDefinition, typeDefinition));

            if (message != null)
                return new ContractMethodWithMessageBuilder(instructionsBuilder, ContractReferences.RequiresWithMessage(moduleDefinition), message);

            return new ContractMethodBuilder(instructionsBuilder, ContractReferences.Requires(moduleDefinition));
        }
    }
}
