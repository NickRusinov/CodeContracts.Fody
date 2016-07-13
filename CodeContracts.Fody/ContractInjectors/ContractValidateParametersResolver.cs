using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.ContractParameterParsers;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Base class for resolves a collection of parameters which can be used for injecting to validation method
    /// </summary>
    public abstract class ContractValidateParametersResolver : IContractValidateParametersResolver
    {
        /// <summary>
        /// Definition of current weaving assembly
        /// </summary>
        private readonly ModuleDefinition moduleDefinition;

        /// <summary>
        /// Parses a top level contract expression
        /// </summary>
        private readonly IMethodParameterParser methodParameterParser;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractValidateParametersResolver"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <param name="methodParameterParser">Parses a top level contract expression</param>
        protected ContractValidateParametersResolver(ModuleDefinition moduleDefinition, IMethodParameterParser methodParameterParser)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(methodParameterParser != null);

            this.moduleDefinition = moduleDefinition;
            this.methodParameterParser = methodParameterParser;
        }

        /// <summary>
        /// Resolves collection of parameters which provides current implementation
        /// </summary>
        /// <param name="contractDefinition">Contract definition for resolves collection of parameters</param>
        /// <returns>
        /// Collection of parameters that represents as <see cref="KeyValuePair{TKey,TValue}"/> where
        /// key is parameter's name; and where value is parameter's value
        /// </returns>
        protected abstract IEnumerable<KeyValuePair<string, object>> ResolveParameters(ContractDefinition contractDefinition);

        /// <inheritdoc/>
        public virtual IEnumerable<ContractValidateParameter> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            foreach (var parameter in ResolveParameters(contractDefinition))
            {
                var parameterString = parameter.Value as string;
                if (parameterString != null)
                {
                    var parseResult = methodParameterParser.Parse(methodDefinition, parameterString);
                    yield return new ContractValidateParameter(new ParameterDefinition(parameter.Key, 0, parseResult.ParsedParameterType), parseResult.ParsedParameterBuilder);
                }
                else
                {
                    var parameterType = moduleDefinition.ImportReference(parameter.Value.GetType());
                    yield return new ContractValidateParameter(new ParameterDefinition(parameter.Key, 0, parameterType), new CompositeParameterBuilder(new ConstParameterBuilder(parameter.Value), new ConvertParameterBuilder(moduleDefinition), new BoxParameterBuilder(moduleDefinition, parameterType)));
                }
            }
        }
    }
}
