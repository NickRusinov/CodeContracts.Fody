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
    [ContractClass(typeof(IEnsuresInjectorContracts))]
    public interface IEnsuresInjector
    {
        void Inject(EnsuresDefinition ensuresDefinition, MethodDefinition methodDefinition);
    }

    [ExcludeFromCodeCoverage]
    [ContractClassFor(typeof(IEnsuresInjector))]
    internal abstract class IEnsuresInjectorContracts : IEnsuresInjector
    {
        public void Inject(EnsuresDefinition ensuresDefinition, MethodDefinition methodDefinition)
        {
            Contract.Requires(ensuresDefinition != null);
            Contract.Requires(methodDefinition != null);

            throw new NotImplementedException();
        }
    }
}
