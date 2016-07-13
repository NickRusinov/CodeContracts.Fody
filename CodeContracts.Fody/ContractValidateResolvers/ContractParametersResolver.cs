using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractParameterParsers;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractValidateResolvers
{
    /// <summary>
    /// Resolves a collection of parameters which representes as constructor's parameters of custom contract attribute 
    /// and can be used for injecting to validation method
    /// </summary>
    public class ContractParametersResolver : ContractValidateParametersResolver
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="ContractParametersResolver"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <param name="methodParameterParser">Parses a top level contract expression</param>
        public ContractParametersResolver(ModuleDefinition moduleDefinition, IMethodParameterParser methodParameterParser)
            : base(moduleDefinition, methodParameterParser)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(methodParameterParser != null);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Current implementation returns collection of constructor's parameters for specified definition of contract
        /// </remarks>
        protected override IEnumerable<KeyValuePair<string, object>> ResolveParameters(ContractDefinition contractDefinition)
        {
            return contractDefinition.ContractAttribute.GetNamedConstructorArguments()
                .Select(cana => new KeyValuePair<string, object>(cana.Name, ((CustomAttributeArgument)cana.Argument.Value).Value));
        }
    }
}
