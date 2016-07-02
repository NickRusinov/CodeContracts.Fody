using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractValidateResolver : IContractValidateResolver
    {
        private readonly IBestOverloadResolver bestOverloadResolver;

        private readonly IContractValidateScanner contractValidateScanner;

        public ContractValidateResolver(IBestOverloadResolver bestOverloadResolver, IContractValidateScanner contractValidateScanner)
        {
            Contract.Requires(bestOverloadResolver != null);
            Contract.Requires(contractValidateScanner != null);

            this.bestOverloadResolver = bestOverloadResolver;
            this.contractValidateScanner = contractValidateScanner;
        }

        public ContractValidate Resolve(CustomAttribute customAttribute, IReadOnlyCollection<ContractMember> contractMembers)
        {
            var contractValidateDefinitions = contractValidateScanner.Scan(customAttribute).ToList();
            var bestOverload = bestOverloadResolver.Resolve(contractValidateDefinitions.Select(cvd => cvd.ValidateMethod).ToList(), contractMembers.Select(cm => cm.ParameterDefinition).ToList());
            
            var contractValidateDefinition = contractValidateDefinitions.First(cvd => Equals(cvd.ValidateMethod, bestOverload));
            var contractParameterBuilders = bestOverload.Parameters.Select(pd => contractMembers.First(cm => Equals(cm.ParameterDefinition.Name, pd.Name))).Select(cm => cm.ParameterBuilder).ToList();

            return new ContractValidate(contractValidateDefinition, contractParameterBuilders);
        }
    }
}
