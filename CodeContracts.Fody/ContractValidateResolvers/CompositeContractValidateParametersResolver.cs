using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractValidateResolvers
{
    /// <summary>
    /// Resolves a collection of parameters which can be used for injecting to validation method
    /// using some <see cref="IContractValidateParametersResolver"/> in series
    /// </summary>
    public class CompositeContractValidateParametersResolver : IContractValidateParametersResolver
    {
        /// <summary>
        /// Collection of inner <see cref="IContractValidateParametersResolver"/>
        /// </summary>
        private readonly IReadOnlyCollection<IContractValidateParametersResolver> contractValidateParametersResolvers;

        /// <summary>
        /// Initializes a new instance of class <see cref="CompositeContractValidateParametersResolver"/>
        /// </summary>
        /// <param name="contractValidateParametersResolvers">Collection of inner <see cref="IContractValidateParametersResolver"/></param>
        public CompositeContractValidateParametersResolver(IReadOnlyCollection<IContractValidateParametersResolver> contractValidateParametersResolvers)
        {
            Contract.Requires(contractValidateParametersResolvers != null);

            this.contractValidateParametersResolvers = contractValidateParametersResolvers;
        }

        /// <summary>
        /// Initializes a new instance of class <see cref="CompositeContractValidateParametersResolver"/>
        /// </summary>
        /// <param name="contractValidateParametersResolvers">Collection of inner <see cref="IContractValidateParametersResolver"/></param>
        public CompositeContractValidateParametersResolver(params IContractValidateParametersResolver[] contractValidateParametersResolvers)
            : this(contractValidateParametersResolvers as IReadOnlyCollection<IContractValidateParametersResolver>)
        {
            Contract.Requires(contractValidateParametersResolvers != null);
        }

        /// <inheritdoc/>
        public IEnumerable<ContractValidateParameter> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            return contractValidateParametersResolvers.SelectMany(cvpr => cvpr.Resolve(contractDefinition, methodDefinition));
        }
    }
}
