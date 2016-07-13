using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractParameterParsers;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractValidateResolvers
{
    /// <summary>
    /// Resolves a collection of parameters which representes as properties of custom contract attribute 
    /// and can be used for injecting to validation method
    /// </summary>
    public class ContractPropertiesResolver : ContractValidateParametersResolver
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="ContractPropertiesResolver"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <param name="methodParameterParser">Parses a top level contract expression</param>
        public ContractPropertiesResolver(ModuleDefinition moduleDefinition, IMethodParameterParser methodParameterParser)
            : base(moduleDefinition, methodParameterParser)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(methodParameterParser != null);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Current implementation returns collection of properties for specified definition of contract
        /// </remarks>
        protected override IEnumerable<KeyValuePair<string, object>> ResolveParameters(ContractDefinition contractDefinition)
        {
            return contractDefinition.ContractAttribute.Properties
                .Select(cana => new KeyValuePair<string, object>(cana.Name, ((CustomAttributeArgument)cana.Argument.Value).Value));
        }
    }
}
