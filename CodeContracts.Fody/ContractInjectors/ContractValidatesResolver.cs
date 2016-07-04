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
    /// Resolves collection of validate methods for injecting to specified method
    /// </summary>
    public class ContractValidatesResolver : IContractValidatesResolver
    {
        /// <summary>
        /// Resolves validate method for injecting to one of many <see cref="Contract"/> class methods
        /// </summary>
        private readonly IContractValidateResolver contractValidateResolver;

        /// <summary>
        /// Resolves a collection of required parameters which can be used for injecting to validation method
        /// </summary>
        private readonly IContractValidateParametersResolver contractParametersResolver;

        /// <summary>
        /// Resolves a collection of optional parameters which can be used for injecting to validation method
        /// </summary>
        private readonly IEnumerable<IContractValidateParametersResolver> contractOptionalParametersResolvers;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractValidatesResolver"/>
        /// </summary>
        /// <param name="contractValidateResolver">Resolves validate method for injecting to one of many <see cref="Contract"/> class methods</param>
        /// <param name="contractParametersResolver">Resolves a collection of required parameters which can be used for injecting to validation method</param>
        /// <param name="contractOptionalParametersResolvers">Resolves a collection of optional parameters which can be used for injecting to validation method</param>
        public ContractValidatesResolver(IContractValidateResolver contractValidateResolver, IContractValidateParametersResolver contractParametersResolver, IEnumerable<IContractValidateParametersResolver> contractOptionalParametersResolvers)
        {
            Contract.Requires(contractValidateResolver != null);
            Contract.Requires(contractParametersResolver != null);
            Contract.Requires(contractOptionalParametersResolvers != null);

            this.contractValidateResolver = contractValidateResolver;
            this.contractParametersResolver = contractParametersResolver;
            this.contractOptionalParametersResolvers = contractOptionalParametersResolvers;
        }

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractValidatesResolver"/>
        /// </summary>
        /// <param name="contractValidateResolver">Resolves validate method for injecting to one of many <see cref="Contract"/> class methods</param>
        /// <param name="contractParametersResolver">Resolves a collection of required parameters which can be used for injecting to validation method</param>
        /// <param name="contractOptionalParametersResolvers">Resolves a collection of optional parameters which can be used for injecting to validation method</param>
        public ContractValidatesResolver(IContractValidateResolver contractValidateResolver, IContractValidateParametersResolver contractParametersResolver, params IContractValidateParametersResolver[] contractOptionalParametersResolvers)
            : this(contractValidateResolver, contractParametersResolver, contractOptionalParametersResolvers as IEnumerable<IContractValidateParametersResolver>)
        {
            Contract.Requires(contractValidateResolver != null);
            Contract.Requires(contractParametersResolver != null);
            Contract.Requires(contractOptionalParametersResolvers != null);
        }

        /// <inheritdoc/>
        public IEnumerable<ContractValidate> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            var extraParametersMembers = contractOptionalParametersResolvers.SelectMany(cmr => cmr.Resolve(contractDefinition, methodDefinition)).ToList();

            return from parameterMember in contractParametersResolver.Resolve(contractDefinition, methodDefinition)
                   let contractParameterDefinitions = parameterMember.Concat(extraParametersMembers)
                   select contractValidateResolver.Resolve(contractDefinition, contractParameterDefinitions.ToList());
        }
    }
}
