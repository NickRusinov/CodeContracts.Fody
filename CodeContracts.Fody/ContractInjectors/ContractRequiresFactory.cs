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
        public IInstructionsBuilder Create(ModuleDefinition moduleDefinition, TypeDefinition typeDefinition, string message)
        {
            if (typeDefinition != null && message != null)
                return new ContractMethodWithMessageBuilder(ContractReferences.RequiresWithExceptionAndMessage(moduleDefinition, typeDefinition), message);

            if (typeDefinition != null)
                return new ContractMethodBuilder(ContractReferences.RequiresWithException(moduleDefinition, typeDefinition));

            if (message != null)
                return new ContractMethodWithMessageBuilder(ContractReferences.RequiresWithMessage(moduleDefinition), message);

            return new ContractMethodBuilder(ContractReferences.Requires(moduleDefinition));
        }
    }
}
