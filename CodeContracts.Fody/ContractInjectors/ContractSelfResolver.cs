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
    public class ContractSelfResolver : IContractMembersResolver
    {
        public IEnumerable<ContractMember> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            if (methodDefinition.IsStatic)
                return new ContractMember(new ParameterDefinition("self", ParameterAttributes.Optional, methodDefinition.Module.TypeSystem.Void), new NullParameterBuilder());

            return new ContractMember(new ParameterDefinition("self", ParameterAttributes.Optional, contractDefinition.DeclaringType), new ThisParameterBuilder());
        }
    }
}
