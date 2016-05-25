using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class Injector : IContractInjector
    {
        private readonly IRequiresInjector requriesInjector;

        private readonly IEnsuresInjector ensuresInjector;

        private readonly IInvariantInjector invariantInjector;

        public Injector(IRequiresInjector requriesInjector, IEnsuresInjector ensuresInjector, IInvariantInjector invariantInjector)
        {
            Contract.Requires(requriesInjector != null);
            Contract.Requires(ensuresInjector != null);
            Contract.Requires(invariantInjector != null);

            this.requriesInjector = requriesInjector;
            this.ensuresInjector = ensuresInjector;
            this.invariantInjector = invariantInjector;
        }

        public void Inject(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            if (contractDefinition is RequiresDefinition)
                requriesInjector.Inject((RequiresDefinition)contractDefinition, methodDefinition);

            else if (contractDefinition is EnsuresDefinition)
                ensuresInjector.Inject((EnsuresDefinition)contractDefinition, methodDefinition);

            else if (contractDefinition is InvariantDefinition)
                invariantInjector.Inject((InvariantDefinition)contractDefinition, methodDefinition);

            else
                throw new NotSupportedException();
        }
    }
}
