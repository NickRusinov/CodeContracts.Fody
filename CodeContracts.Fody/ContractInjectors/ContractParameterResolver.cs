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
    public class ContractParameterResolver : IContractParameterResolver
    {
        private readonly IMethodParameterParser methodParameterParser;

        private readonly IConstParameterBuilderFactory constParameterBuilderFactory;

        public ContractParameterResolver(IMethodParameterParser methodParameterParser, IConstParameterBuilderFactory constParameterBuilderFactory)
        {
            Contract.Requires(methodParameterParser != null);
            Contract.Requires(constParameterBuilderFactory != null);

            this.methodParameterParser = methodParameterParser;
            this.constParameterBuilderFactory = constParameterBuilderFactory;
        }

        public IParameterBuilder Resolve(ContractParameterDefinition contractParameterDefinition, MethodDefinition methodDefinition)
        {
            if (contractParameterDefinition.ParameterValue is string)
                return new CompositeParameterBuilder(methodParameterParser.Parse(methodDefinition, (string)contractParameterDefinition.ParameterValue));

            return constParameterBuilderFactory.Create(methodDefinition.Module, contractParameterDefinition.ParameterValue);
        }
    }
}
