using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.BestOverloadResolvers;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractValidateResolvers
{
    /// <summary>
    /// Resolves validate method for injecting to one of many <see cref="Contract"/> class methods
    /// </summary>
    public class ContractValidateResolver : IContractValidateResolver
    {
        /// <summary>
        /// Resolves method that is a best overload for specified parameters
        /// </summary>
        private readonly IBestOverloadResolver bestOverloadResolver;

        /// <summary>
        /// Scans validate methods in a custom attribute
        /// </summary>
        private readonly IContractValidateScanner contractValidateScanner;

        /// <summary>
        /// Resolves a collection of required parameters which can be used for injecting to validation method
        /// </summary>
        private readonly IContractValidateParametersResolver contractParametersResolver;

        /// <summary>
        /// Initailizes a new instance of class <see cref="ContractValidateResolver"/>
        /// </summary>
        /// <param name="bestOverloadResolver">Resolves method that is a best overload for specified parameters</param>
        /// <param name="contractValidateScanner">Scans validate methods in a custom attribute</param>
        /// <param name="contractParametersResolver">Resolves a collection of required parameters which can be used for injecting to validation method</param>
        public ContractValidateResolver(IBestOverloadResolver bestOverloadResolver, IContractValidateScanner contractValidateScanner, IContractValidateParametersResolver contractParametersResolver)
        {
            Contract.Requires(bestOverloadResolver != null);
            Contract.Requires(contractValidateScanner != null);
            Contract.Requires(contractParametersResolver != null);

            this.bestOverloadResolver = bestOverloadResolver;
            this.contractValidateScanner = contractValidateScanner;
            this.contractParametersResolver = contractParametersResolver;
        }

        /// <inheritdoc/>
        public ContractValidate Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            var contractValidateDefinitions = contractValidateScanner.Scan(contractDefinition.ContractAttribute).ToList();
            var contractValidateParameters = contractParametersResolver.Resolve(contractDefinition, methodDefinition).ToList();
            var bestOverload = bestOverloadResolver.Resolve(contractValidateDefinitions.Select(cvd => cvd.ValidateMethod).ToList(), contractValidateParameters.Select(cm => cm.ParameterDefinition).ToList());
            
            var contractValidateDefinition = contractValidateDefinitions.First(cvd => Equals(cvd.ValidateMethod, bestOverload));
            var contractParameterBuilders = bestOverload.Parameters.Select(pd => contractValidateParameters.First(cm => StringComparer.OrdinalIgnoreCase.Equals(cm.ParameterDefinition.Name, pd.Name))).Select(cm => cm.ParameterBuilder).ToList();

            return new ContractValidate(contractValidateDefinition, contractParameterBuilders);
        }
    }
}
