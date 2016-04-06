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
    [ContractClass(typeof(IInvariantInjectorContracts))]
    public interface IInvariantInjector
    {
        void Inject(ILProcessor ilProcessor, InvariantDefinition invariantDefinition);
    }

    [ExcludeFromCodeCoverage]
    [ContractClassFor(typeof(IInvariantInjector))]
    internal abstract class IInvariantInjectorContracts : IInvariantInjector
    {
        public void Inject(ILProcessor ilProcessor, InvariantDefinition invariantDefinition)
        {
            Contract.Requires(ilProcessor != null);
            Contract.Requires(invariantDefinition != null);

            throw new NotImplementedException();
        }
    }
}
