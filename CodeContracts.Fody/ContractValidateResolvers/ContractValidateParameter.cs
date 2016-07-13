using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractParameterBuilders;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractValidateResolvers
{
    /// <summary>
    /// Represents union of parameter of validate method that can be used in contract expression and 
    /// builder of il instructions for its parameter
    /// </summary>
    public struct ContractValidateParameter : IEnumerable<ContractValidateParameter>
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="ContractValidateParameter"/>
        /// </summary>
        /// <param name="parameterDefinition">Parameter of validate method that can be used in contract expression</param>
        /// <param name="parameterBuilder">Builder of il instructions for <paramref name="parameterDefinition"/>'s parameters</param>
        public ContractValidateParameter(ParameterDefinition parameterDefinition, IParameterBuilder parameterBuilder)
        {
            Contract.Requires(parameterDefinition != null);
            Contract.Requires(parameterBuilder != null);

            ParameterDefinition = parameterDefinition;
            ParameterBuilder = parameterBuilder;
        }

        /// <summary>
        /// Parameter of validate method that can be used in contract expression
        /// </summary>
        public ParameterDefinition ParameterDefinition { get; }

        /// <summary>
        /// Builder of il instructions for <see cref="ParameterDefinition"/>'s parameters
        /// </summary>
        public IParameterBuilder ParameterBuilder { get; }

        /// <inheritdoc/>
        public IEnumerator<ContractValidateParameter> GetEnumerator() => Enumerable.Repeat(this, 1).GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
