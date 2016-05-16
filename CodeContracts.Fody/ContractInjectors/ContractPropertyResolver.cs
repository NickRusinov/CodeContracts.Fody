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
    public class ContractPropertyResolver : IContractPropertyResolver
    {
        private readonly IMethodParameterParser methodParameterParser;

        private readonly IConstParameterBuilderFactory constParameterBuilderFactory;

        public ContractPropertyResolver(IMethodParameterParser methodParameterParser, IConstParameterBuilderFactory constParameterBuilderFactory)
        {
            Contract.Requires(methodParameterParser != null);
            Contract.Requires(constParameterBuilderFactory != null);

            this.methodParameterParser = methodParameterParser;
            this.constParameterBuilderFactory = constParameterBuilderFactory;
        }

        public IParameterBuilder Resolve(ContractPropertyDefinition contractPropertyDefinition, MethodDefinition methodDefinition)
        {
            if (contractPropertyDefinition.PropertyValue is string)
                return new CompositeParameterBuilder(methodParameterParser.Parse(methodDefinition, (string)contractPropertyDefinition.PropertyValue));

            return constParameterBuilderFactory.Create(methodDefinition.Module, contractPropertyDefinition.PropertyValue);
        }
    }
}
