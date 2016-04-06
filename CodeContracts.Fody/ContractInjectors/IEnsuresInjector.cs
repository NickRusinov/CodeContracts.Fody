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
    [ContractClass(typeof(IEnsuresInjectorContracts))]
    public interface IEnsuresInjector
    {
        void Inject(ILProcessor ilProcessor, EnsuresDefinition ensuresDefinition);
    }

    [ExcludeFromCodeCoverage]
    [ContractClassFor(typeof(IEnsuresInjector))]
    internal abstract class IEnsuresInjectorContracts : IEnsuresInjector
    {
        public void Inject(ILProcessor ilProcessor, EnsuresDefinition ensuresDefinition)
        {
            Contract.Requires(ilProcessor != null);
            Contract.Requires(ensuresDefinition != null);

            throw new NotImplementedException();
        }
    }
}
