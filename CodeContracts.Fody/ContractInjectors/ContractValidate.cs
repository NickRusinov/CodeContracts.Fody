using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractParameterBuilders;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Represents union of method of contract attribute that can be used in contract expression and 
    /// builders of il instructions for its parameters   
    /// </summary>
    public struct ContractValidate : IEnumerable<ContractValidate>
    {
        /// <summary>
        /// Initializes a new instance of struct <see cref="ContractValidate"/>
        /// </summary>
        /// <param name="validateDefinition">Method of contract attribute that can be used in contract expression</param>
        /// <param name="parameterBuilders">Collection of builders of il instructions for <paramref name="validateDefinition"/>'s parameters</param>
        public ContractValidate(ContractValidateDefinition validateDefinition, IReadOnlyCollection<IParameterBuilder> parameterBuilders)
        {
            Contract.Requires(validateDefinition != null);
            Contract.Requires(parameterBuilders != null);

            ValidateDefinition = validateDefinition;
            ParameterBuilders = parameterBuilders;
        }

        /// <summary>
        /// Method of contract attribute that can be used in contract expression
        /// </summary>
        public ContractValidateDefinition ValidateDefinition { get; }

        /// <summary>
        /// Collection of builders of il instructions for <see cref="ValidateDefinition"/>'s parameters
        /// </summary>
        public IReadOnlyCollection<IParameterBuilder> ParameterBuilders { get; }

        /// <inheritdoc/>
        public IEnumerator<ContractValidate> GetEnumerator() => Enumerable.Repeat(this, 1).GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
