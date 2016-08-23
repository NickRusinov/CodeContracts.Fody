using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.ContractValidateResolvers;
using CodeContracts.Fody.Internal;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractValidateResolvers
{
    public class ContractAttributeResolverTests
    {
        [Theory(DisplayName = "Проверка разрешения параметра атрибута контракта, примененного к полю"), AutoFixture]
        public void AttributeAppliedToFieldTest(
            [Frozen] ModuleDefinition moduleDefinition,
            MethodDefinition methodDefinition,
            ContractAttributeResolver sut)
        {
            var fieldDefinition = moduleDefinition.FindField("ConcreteClassWithField", "fieldWithAttribute");
            var contractDefinition = new InvariantDefinition(fieldDefinition.CustomAttributes.Single(), fieldDefinition, fieldDefinition.DeclaringType);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();

            var builders = ContractValidateParametersAssert(contractValidateParameters, fieldDefinition.FieldType).ToList();
            Assert.IsType<ThisParameterBuilder>(builders[0]);
            Assert.IsType<FieldParameterBuilder>(builders[1]);
            Assert.IsType<ConvertParameterBuilder>(builders[2]);
            Assert.IsType<BoxParameterBuilder>(builders[3]);
        }

        [Theory(DisplayName = "Проверка разрешения параметра атрибута контракта, примененного к свойству"), AutoFixture]
        public void AttributeAppliedToPropertyRequiresTest(
            [Frozen] ModuleDefinition moduleDefinition,
            MethodDefinition methodDefinition,
            ContractAttributeResolver sut)
        {
            var propertyDefinition = moduleDefinition.FindProperty("ConcreteClassWithProperty", "PropertyWithAttribute");
            var contractDefinition = new RequiresDefinition(propertyDefinition.CustomAttributes.Single(), propertyDefinition, propertyDefinition.SetMethod);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();

            var builders = ContractValidateParametersAssert(contractValidateParameters, propertyDefinition.PropertyType).ToList();
            Assert.IsType<ArgumentParameterBuilder>(builders[0]);
            Assert.IsType<ConvertParameterBuilder>(builders[1]);
            Assert.IsType<BoxParameterBuilder>(builders[2]);
        }

        [Theory(DisplayName = "Проверка разрешения параметра атрибута контракта, примененного к свойству"), AutoFixture]
        public void AttributeAppliedToPropertyEnsuresTest(
            [Frozen] ModuleDefinition moduleDefinition,
            MethodDefinition methodDefinition,
            ContractAttributeResolver sut)
        {
            var propertyDefinition = moduleDefinition.FindProperty("ConcreteClassWithProperty", "PropertyWithAttribute");
            var contractDefinition = new EnsuresDefinition(propertyDefinition.CustomAttributes.Single(), propertyDefinition, propertyDefinition.GetMethod);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();

            var builders = ContractValidateParametersAssert(contractValidateParameters, propertyDefinition.PropertyType).ToList();
            Assert.IsType<ResultParameterBuilder>(builders[0]);
            Assert.IsType<ConvertParameterBuilder>(builders[1]);
            Assert.IsType<BoxParameterBuilder>(builders[2]);
        }

        [Theory(DisplayName = "Проверка разрешения параметра атрибута контракта, примененного к параметру метода"), AutoFixture]
        public void AttributeAppliedToParameterTest(
            [Frozen] ModuleDefinition moduleDefinition,
            MethodDefinition methodDefinition,
            ContractAttributeResolver sut)
        {
            var parameterDefinition = moduleDefinition.FindParameter("ConcreteClass", "MethodWithParameter", "parameter");
            var contractDefinition = new RequiresDefinition(parameterDefinition.CustomAttributes.Single(), parameterDefinition, (MethodDefinition)parameterDefinition.Method);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();

            var builders = ContractValidateParametersAssert(contractValidateParameters, parameterDefinition.ParameterType).ToList();
            Assert.IsType<ArgumentParameterBuilder>(builders[0]);
            Assert.IsType<ConvertParameterBuilder>(builders[1]);
            Assert.IsType<BoxParameterBuilder>(builders[2]);
        }

        [Theory(DisplayName = "Проверка разрешения параметра атрибута контракта, примененного к возвращаемому значению метода"), AutoFixture]
        public void AttributeAppliedToReturnTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractAttributeResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("ConcreteClass", "MethodWithReturnAttribute");
            var contractDefinition = new EnsuresDefinition(methodDefinition.MethodReturnType.CustomAttributes.Single(), methodDefinition.MethodReturnType, methodDefinition);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();

            var builders = ContractValidateParametersAssert(contractValidateParameters, methodDefinition.MethodReturnType.ReturnType).ToList();
            Assert.IsType<ResultParameterBuilder>(builders[0]);
            Assert.IsType<ConvertParameterBuilder>(builders[1]);
            Assert.IsType<BoxParameterBuilder>(builders[2]);
        }

        private static IEnumerable<IParameterBuilder> ContractValidateParametersAssert(IReadOnlyCollection<ContractValidateParameter> contractValidateParameters, TypeReference parameterTypeReference)
        {
            var contractValidateParameter = Assert.Single(contractValidateParameters);
            var compositeParameterBuilder = Assert.IsType<CompositeParameterBuilder>(contractValidateParameter.ParameterBuilder);
            Assert.Equal("arg", contractValidateParameter.ParameterDefinition.Name);
            Assert.Equal(parameterTypeReference, contractValidateParameter.ParameterDefinition.ParameterType, TypeReferenceComparer.Instance);

            return compositeParameterBuilder.FindPrivateField<IEnumerable<IParameterBuilder>>("parameterBuilders");
        }
    }
}
