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
    /// <summary>
    /// Injectes calls of methods of <see cref="Contract"/> class to specified methods
    /// </summary>
    public class ContractInjector : IContractInjector
    {
        /// <summary>
        /// Injects il instructions for requries expression in specified method
        /// </summary>
        private readonly IRequiresInjector requriesInjector;

        /// <summary>
        /// Injects il instructions for ensures expression in specified method
        /// </summary>
        private readonly IEnsuresInjector ensuresInjector;

        /// <summary>
        /// Injects il instructions for invariant expression in specified method
        /// </summary>
        private readonly IInvariantInjector invariantInjector;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractInjector"/>
        /// </summary>
        /// <param name="requriesInjector">Injects il instructions for requries expression in specified method</param>
        /// <param name="ensuresInjector">Injects il instructions for ensures expression in specified method</param>
        /// <param name="invariantInjector">Injects il instructions for invariant expression in specified method</param>
        public ContractInjector(IRequiresInjector requriesInjector, IEnsuresInjector ensuresInjector, IInvariantInjector invariantInjector)
        {
            Contract.Requires(requriesInjector != null);
            Contract.Requires(ensuresInjector != null);
            Contract.Requires(invariantInjector != null);

            this.requriesInjector = requriesInjector;
            this.ensuresInjector = ensuresInjector;
            this.invariantInjector = invariantInjector;
        }

        /// <inheritdoc/>
        /// <exception cref="NotSupportedException">
        /// Throws if contract definition isn't requires, ensures or invariant definition 
        /// (other definitions are not allowed)
        /// </exception>
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
