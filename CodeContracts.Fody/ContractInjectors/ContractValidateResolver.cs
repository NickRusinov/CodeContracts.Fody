using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;

namespace CodeContracts.Fody.ContractInjectors
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
        /// Initailizes a new instance of class <see cref="ContractValidateResolver"/>
        /// </summary>
        /// <param name="bestOverloadResolver">Resolves method that is a best overload for specified parameters</param>
        /// <param name="contractValidateScanner">Scans validate methods in a custom attribute</param>
        public ContractValidateResolver(IBestOverloadResolver bestOverloadResolver, IContractValidateScanner contractValidateScanner)
        {
            Contract.Requires(bestOverloadResolver != null);
            Contract.Requires(contractValidateScanner != null);

            this.bestOverloadResolver = bestOverloadResolver;
            this.contractValidateScanner = contractValidateScanner;
        }

        /// <inheritdoc/>
        public ContractValidate Resolve(ContractDefinition contractDefinition, IReadOnlyCollection<ContractValidateParameter> contractValidateParameters)
        {
            var contractValidateDefinitions = contractValidateScanner.Scan(contractDefinition.ContractAttribute).ToList();
            var bestOverload = bestOverloadResolver.Resolve(contractValidateDefinitions.Select(cvd => cvd.ValidateMethod).ToList(), contractValidateParameters.Select(cm => cm.ParameterDefinition).ToList());
            
            var contractValidateDefinition = contractValidateDefinitions.First(cvd => Equals(cvd.ValidateMethod, bestOverload));
            var contractParameterBuilders = bestOverload.Parameters.Select(pd => contractValidateParameters.First(cm => Equals(cm.ParameterDefinition.Name, pd.Name))).Select(cm => cm.ParameterBuilder).ToList();

            return new ContractValidate(contractValidateDefinition, contractParameterBuilders);
        }
    }
}
