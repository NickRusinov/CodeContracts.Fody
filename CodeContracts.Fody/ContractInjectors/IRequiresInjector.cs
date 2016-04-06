using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    [ContractClass(typeof(IRequiresInjectorContracts))]
    public interface IRequiresInjector
    {
        void Inject(ILProcessor ilProcessor, RequiresDefinition requiresDefinition);
    }

    [ExcludeFromCodeCoverage]
    [ContractClassFor(typeof(IRequiresInjector))]
    internal abstract class IRequiresInjectorContracts : IRequiresInjector
    {
        public void Inject(ILProcessor ilProcessor, RequiresDefinition requiresDefinition)
        {
            Contract.Requires(ilProcessor != null);
            Contract.Requires(requiresDefinition != null);

            throw new NotImplementedException();
        }
    }
}
