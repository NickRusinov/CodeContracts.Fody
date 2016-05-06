using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    [ContractClass(typeof(IRequiresInjectorContracts))]
    public interface IRequiresInjector
    {
        void Inject(RequiresDefinition requiresDefinition, MethodDefinition methodDefinition);
    }

    [ExcludeFromCodeCoverage]
    [ContractClassFor(typeof(IRequiresInjector))]
    internal abstract class IRequiresInjectorContracts : IRequiresInjector
    {
        public void Inject(RequiresDefinition requiresDefinition, MethodDefinition methodDefinition)
        {
            Contract.Requires(requiresDefinition != null);
            Contract.Requires(methodDefinition != null);

            throw new NotImplementedException();
        }
    }
}
