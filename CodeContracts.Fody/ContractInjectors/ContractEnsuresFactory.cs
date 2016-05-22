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
        public IInstructionsBuilder Create(ModuleDefinition moduleDefinition, TypeDefinition typeDefinition, string message)
        {
            if (message != null)
                return new ContractMethodWithMessageBuilder(ContractReferences.EnsuresWithMessage(moduleDefinition), message);

            return new ContractMethodBuilder(ContractReferences.Ensures(moduleDefinition));
        }
    }
}
