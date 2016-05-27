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
    public class ContractValidatesResolver : IContractValidatesResolver
    {
        private readonly IContractValidateResolver contractValidateResolver;

        private readonly IContractMembersResolver contractParametersResolver;

        private readonly IEnumerable<IContractMembersResolver> contractExtraParametersResolvers;

        public ContractValidatesResolver(IContractValidateResolver contractValidateResolver, IContractMembersResolver contractParametersResolver, IEnumerable<IContractMembersResolver> contractExtraParametersResolvers)
        {
            Contract.Requires(contractValidateResolver != null);
            Contract.Requires(contractParametersResolver != null);
            Contract.Requires(contractExtraParametersResolvers != null);

            this.contractValidateResolver = contractValidateResolver;
            this.contractParametersResolver = contractParametersResolver;
            this.contractExtraParametersResolvers = contractExtraParametersResolvers;
        }

        public ContractValidatesResolver(IContractValidateResolver contractValidateResolver, IContractMembersResolver contractParametersResolver, params IContractMembersResolver[] contractExtraParametersResolvers)
            : this(contractValidateResolver, contractParametersResolver, contractExtraParametersResolvers as IEnumerable<IContractMembersResolver>)
        {
            Contract.Requires(contractValidateResolver != null);
            Contract.Requires(contractParametersResolver != null);
            Contract.Requires(contractExtraParametersResolvers != null);
        }

        public IEnumerable<ContractValidate> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            var extraParametersMembers = contractExtraParametersResolvers.SelectMany(cmr => cmr.Resolve(contractDefinition, methodDefinition)).ToList();

            return from parameterMember in contractParametersResolver.Resolve(contractDefinition, methodDefinition)
                   let contractParameterDefinitions = parameterMember.Concat(extraParametersMembers)
                   select contractValidateResolver.Resolve(contractDefinition.ContractAttribute, contractParameterDefinitions.ToList());
        }
    }
}
