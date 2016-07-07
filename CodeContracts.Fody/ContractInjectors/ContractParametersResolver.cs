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
        /// if parameter of constructorrepresentes as array 
        /// </remarks>
        protected override IEnumerable<KeyValuePair<string, object>> ResolveParameters(ContractDefinition contractDefinition)
        {
            return contractDefinition.ContractAttribute.ConstructorArguments.Select(caa => (IEnumerable<CustomAttributeArgument>)caa.Value).Single()
                .Select(caa => ((CustomAttributeArgument)caa.Value).Value)
                .Select(o => new KeyValuePair<string, object>("arg", o));
        }

        /// <inheritdoc/>
        /// <remarks>
        /// If specified contract attribute applied to field, property or parameter of method then 
        /// specifies constructor's parameters of contract attribute are not allowed
        /// </remarks>
        public override IEnumerable<ContractValidateParameter> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            var fieldDefinition = contractDefinition.AttributeProvider as FieldDefinition;
            if (fieldDefinition != null)
                return new ContractValidateParameter(new ParameterDefinition("arg", 0, fieldDefinition.FieldType), new CompositeParameterBuilder(new FieldParameterBuilder(fieldDefinition), new BoxParameterBuilder(moduleDefinition, fieldDefinition.FieldType)));

            var propertyDefinition = contractDefinition.AttributeProvider as PropertyDefinition;
            if (propertyDefinition != null)
                return new ContractValidateParameter(new ParameterDefinition("arg", 0, propertyDefinition.PropertyType), new CompositeParameterBuilder(new PropertyParameterBuilder(propertyDefinition), new BoxParameterBuilder(moduleDefinition, propertyDefinition.PropertyType)));

            var parameterDefinition = contractDefinition.AttributeProvider as ParameterDefinition;
            if (parameterDefinition != null)
                return new ContractValidateParameter(new ParameterDefinition("arg", 0, parameterDefinition.ParameterType), new CompositeParameterBuilder(new ArgumentParameterBuilder(parameterDefinition), new BoxParameterBuilder(moduleDefinition, parameterDefinition.ParameterType)));

            return base.Resolve(contractDefinition, methodDefinition);
        }
    }
}
