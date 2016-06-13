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
    public class ContractPropertiesResolver : ContractMembersResolver
    {
        public ContractPropertiesResolver(ModuleDefinition moduleDefinition, IMethodParameterParser methodParameterParser)
            : base(moduleDefinition, methodParameterParser)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(methodParameterParser != null);
        }

        protected override IEnumerable<KeyValuePair<string, object>> ResolveParameters(ContractDefinition contractDefinition)
        {
            return contractDefinition.ContractAttribute.Properties
                .Select(cana => new KeyValuePair<string, object>(cana.Name, ((CustomAttributeArgument)cana.Argument.Value).Value));
        }
    }
}
