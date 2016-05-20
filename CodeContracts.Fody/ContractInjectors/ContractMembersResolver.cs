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
    public abstract class ContractMembersResolver : IContractMembersResolver
    {
        private readonly IMethodParameterParser methodParameterParser;

        protected ContractMembersResolver(IMethodParameterParser methodParameterParser)
        {
            Contract.Requires(methodParameterParser != null);

            this.methodParameterParser = methodParameterParser;
        }

        protected abstract IEnumerable<KeyValuePair<string, object>> ResolveParameters(ContractDefinition contractDefinition);

        public virtual IEnumerable<ContractMember> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            foreach (var parameter in ResolveParameters(contractDefinition))
            {
                var parameterString = parameter.Value as string;
                if (parameterString != null)
                {
                    var parseResult = methodParameterParser.Parse(methodDefinition, parameterString);
                    yield return new ContractMember(new ParameterDefinition(parameter.Key, 0, parseResult.ParsedParameterType), parseResult.ParsedParameterBuilder);
                }
                else
                    yield return new ContractMember(new ParameterDefinition(parameter.Key, 0, methodDefinition.Module.ImportReference(parameter.Value.GetType())), new ConstParameterBuilder(parameter.Value)); 
            }
        }
    }
}
