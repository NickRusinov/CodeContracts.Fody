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
    [ContractClass(typeof(IInvariantInjectorContracts))]
    public interface IInvariantInjector
    {
        void Inject(InvariantDefinition invariantDefinition, MethodDefinition methodDefinition);
    }

    [ExcludeFromCodeCoverage]
    [ContractClassFor(typeof(IInvariantInjector))]
    internal abstract class IInvariantInjectorContracts : IInvariantInjector
    {
        public void Inject(InvariantDefinition invariantDefinition, MethodDefinition methodDefinition)
        {
            Contract.Requires(invariantDefinition != null);
            Contract.Requires(methodDefinition != null);

            throw new NotImplementedException();
        }
    }
}
