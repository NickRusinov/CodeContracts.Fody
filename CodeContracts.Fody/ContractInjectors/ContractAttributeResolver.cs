using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractParameterBuilders;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Resolves parameter that representes as member to which was applied custom attribute
    /// and can be used for injecting to validation method
    /// </summary>
    public class ContractAttributeResolver : IContractValidateParametersResolver
    {
        /// <summary>
        /// Definition of current weaving assembly
        /// </summary>
        private readonly ModuleDefinition moduleDefinition;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractAttributeResolver"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        public ContractAttributeResolver(ModuleDefinition moduleDefinition)
        {
            Contract.Requires(moduleDefinition != null);

            this.moduleDefinition = moduleDefinition;
        }

        /// <inheritdoc/>
        public IEnumerable<ContractValidateParameter> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            var parameterDefinition = contractDefinition.AttributeProvider as ParameterDefinition;
            if (parameterDefinition != null)
                return new ContractValidateParameter(new ParameterDefinition("arg", 0, parameterDefinition.ParameterType), new CompositeParameterBuilder(new ArgumentParameterBuilder(parameterDefinition), new BoxParameterBuilder(moduleDefinition, parameterDefinition.ParameterType)));

            var methodReturnDefinition = contractDefinition.AttributeProvider as MethodReturnType;
            if (methodReturnDefinition != null)
                return new ContractValidateParameter(new ParameterDefinition("arg", 0, methodReturnDefinition.ReturnType), new CompositeParameterBuilder(new ResultParameterBuilder(moduleDefinition, methodReturnDefinition.ReturnType), new BoxParameterBuilder(moduleDefinition, methodReturnDefinition.ReturnType)));

            var fieldDefinition = contractDefinition.AttributeProvider as FieldDefinition;
            if (fieldDefinition != null)
                return new ContractValidateParameter(new ParameterDefinition("arg", 0, fieldDefinition.FieldType), new CompositeParameterBuilder(new ThisParameterBuilder(), new FieldParameterBuilder(fieldDefinition), new BoxParameterBuilder(moduleDefinition, fieldDefinition.FieldType)));

            var propertyDefinition = contractDefinition.AttributeProvider as PropertyDefinition;
            if (propertyDefinition != null && contractDefinition is RequiresDefinition)
                return new ContractValidateParameter(new ParameterDefinition("arg", 0, propertyDefinition.PropertyType), new CompositeParameterBuilder(new ArgumentParameterBuilder(propertyDefinition.SetMethod.Parameters.Single()), new BoxParameterBuilder(moduleDefinition, propertyDefinition.PropertyType)));

            if (propertyDefinition != null && contractDefinition is EnsuresDefinition)
                return new ContractValidateParameter(new ParameterDefinition("arg", 0, propertyDefinition.PropertyType), new CompositeParameterBuilder(new ResultParameterBuilder(moduleDefinition, propertyDefinition.PropertyType), new BoxParameterBuilder(moduleDefinition, propertyDefinition.PropertyType)));

            if (propertyDefinition != null && contractDefinition is InvariantDefinition)
                return new ContractValidateParameter(new ParameterDefinition("arg", 0, propertyDefinition.PropertyType), new CompositeParameterBuilder(new ThisParameterBuilder(), new PropertyParameterBuilder(propertyDefinition), new BoxParameterBuilder(moduleDefinition, propertyDefinition.PropertyType)));

            return Enumerable.Empty<ContractValidateParameter>();
        }
    }
}
