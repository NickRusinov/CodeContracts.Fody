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
    public class ContractParametersResolver : ContractMembersResolver
    {
        public ContractParametersResolver(ModuleDefinition moduleDefinition, IMethodParameterParser methodParameterParser)
            : base(moduleDefinition, methodParameterParser)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(methodParameterParser != null);
        }

        protected override IEnumerable<KeyValuePair<string, object>> ResolveParameters(ContractDefinition contractDefinition)
        {
            return contractDefinition.ContractAttribute.ConstructorArguments.Select(caa => (IEnumerable<CustomAttributeArgument>)caa.Value).Single()
                .Select(caa => ((CustomAttributeArgument)caa.Value).Value)
                .Select(o => new KeyValuePair<string, object>("arg", o));
        }

        public override IEnumerable<ContractMember> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            var fieldDefinition = contractDefinition.AttributeProvider as FieldDefinition;
            if (fieldDefinition != null)
                return new ContractMember(new ParameterDefinition("arg", 0, fieldDefinition.FieldType), new FieldParameterBuilder(fieldDefinition));

            var propertyDefinition = contractDefinition.AttributeProvider as PropertyDefinition;
            if (propertyDefinition != null)
                return new ContractMember(new ParameterDefinition("arg", 0, propertyDefinition.PropertyType), new PropertyParameterBuilder(propertyDefinition));

            var parameterDefinition = contractDefinition.AttributeProvider as ParameterDefinition;
            if (parameterDefinition != null)
                return new ContractMember(new ParameterDefinition("arg", 0, parameterDefinition.ParameterType), new ArgumentParameterBuilder(parameterDefinition));

            return base.Resolve(contractDefinition, methodDefinition);
        }
    }
}
