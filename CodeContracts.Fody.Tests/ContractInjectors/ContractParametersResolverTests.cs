using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.Internal;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ContractParametersResolverTests
    {
        [Theory(DisplayName = "Проверка разрешения константного параметра атрибута контракта"), AutoFixture]
        public void ConstIfParameterValueIsConstTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractParametersResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthMaul", "JoinDarkSide");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();
            var constMember = contractValidateParameters.Skip(1).First();

            Assert.Equal(2, contractValidateParameters.Count);
            Assert.Equal("arg", constMember.ParameterDefinition.Name);
            Assert.Equal(moduleDefinition.TypeSystem.UInt64, constMember.ParameterDefinition.ParameterType, TypeReferenceComparer.Instance);
            Assert.IsType<CompositeParameterBuilder>(constMember.ParameterBuilder);
        }

        [Theory(DisplayName = "Проверка разрешения строкового параметра атрибута контракта"), AutoFixture]
        public void ParseIfParameterValueIsStringTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] Mock<IMethodParameterParser> methodParameterParserMock,
            ContractParametersResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthMaul", "JoinDarkSide");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();
            var stringMember = contractValidateParameters.First();

            methodParameterParserMock.Verify(mpp => mpp.Parse(methodDefinition, "$parameter"), Times.Once);
            Assert.Equal(2, contractValidateParameters.Count);
            Assert.Equal("arg", stringMember.ParameterDefinition.Name);
        }

        [Theory(DisplayName = "Проверка разрешения параметра атрибута контракта, примененного к полю"), AutoFixture]
        public void AttributeAppliedToFieldTest(
            [Frozen] ModuleDefinition moduleDefinition,
            MethodDefinition methodDefinition,
            ContractParametersResolver sut)
        {
            var fieldDefinition = moduleDefinition.FindField("DarthMaul", "lightsaber");
            var contractDefinition = new InvariantDefinition(fieldDefinition.CustomAttributes.Single(), fieldDefinition, fieldDefinition.DeclaringType);
            
            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();
            
            var contractValidateParameter = Assert.Single(contractValidateParameters);
            var compositeParameterBuilder = Assert.IsType<CompositeParameterBuilder>(contractValidateParameter.ParameterBuilder);
            Assert.Equal("arg", contractValidateParameter.ParameterDefinition.Name);
            Assert.Equal(fieldDefinition.FieldType, contractValidateParameter.ParameterDefinition.ParameterType, TypeReferenceComparer.Instance);
            Assert.IsType<FieldParameterBuilder>(compositeParameterBuilder.FindPrivateField<IEnumerable<IParameterBuilder>>("parameterBuilders").First());
        }

        [Theory(DisplayName = "Проверка разрешения параметра атрибута контракта, примененного к свойству"), AutoFixture]
        public void AttributeAppliedToPropertyTest(
            [Frozen] ModuleDefinition moduleDefinition,
            MethodDefinition methodDefinition,
            ContractParametersResolver sut)
        {
            var propertyDefinition = moduleDefinition.FindProperty("DarthPlagueis", "Slave");
            var contractDefinition = new InvariantDefinition(propertyDefinition.CustomAttributes.Single(), propertyDefinition, propertyDefinition.DeclaringType);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();

            var contractValidateParameter = Assert.Single(contractValidateParameters);
            var compositeParameterBuilder = Assert.IsType<CompositeParameterBuilder>(contractValidateParameter.ParameterBuilder);
            Assert.Equal("arg", contractValidateParameter.ParameterDefinition.Name);
            Assert.Equal(propertyDefinition.PropertyType, contractValidateParameter.ParameterDefinition.ParameterType, TypeReferenceComparer.Instance);
            Assert.IsType<PropertyParameterBuilder>(compositeParameterBuilder.FindPrivateField<IEnumerable<IParameterBuilder>>("parameterBuilders").First());
        }

        [Theory(DisplayName = "Проверка разрешения параметра атрибута контракта, примененного к параметру метода"), AutoFixture]
        public void AttributeAppliedToParameterTest(
            [Frozen] ModuleDefinition moduleDefinition,
            MethodDefinition methodDefinition,
            ContractParametersResolver sut)
        {
            var parameterDefinition = moduleDefinition.FindParameter("DarthMaul", "KillJedi", "jedi");
            var contractDefinition = new RequiresDefinition(parameterDefinition.CustomAttributes.Single(), parameterDefinition, (MethodDefinition)parameterDefinition.Method);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();

            var contractValidateParameter = Assert.Single(contractValidateParameters);
            var compositeParameterBuilder = Assert.IsType<CompositeParameterBuilder>(contractValidateParameter.ParameterBuilder);
            Assert.Equal("arg", contractValidateParameter.ParameterDefinition.Name);
            Assert.Equal(parameterDefinition.ParameterType, contractValidateParameter.ParameterDefinition.ParameterType, TypeReferenceComparer.Instance);
            Assert.IsType<ArgumentParameterBuilder>(compositeParameterBuilder.FindPrivateField<IEnumerable<IParameterBuilder>>("parameterBuilders").First());
        }
    }
}
