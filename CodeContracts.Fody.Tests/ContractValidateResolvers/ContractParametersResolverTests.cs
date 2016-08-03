using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.ContractParameterParsers;
using CodeContracts.Fody.ContractValidateResolvers;
using CodeContracts.Fody.Internal;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractValidateResolvers
{
    public class ContractParametersResolverTests
    {
        [Theory(DisplayName = "Проверка разрешения константного параметра атрибута контракта"), AutoFixture]
        public void ConstIfParameterValueIsConstTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractParametersResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("ConcreteClassWithParametersAttributes", "Method");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();

            Assert.Equal(2, contractValidateParameters.Count);
            var constMember = Assert.Single(contractValidateParameters, cm => cm.ParameterDefinition.Name == "x");
            Assert.Equal(moduleDefinition.TypeSystem.Int32, constMember.ParameterDefinition.ParameterType, TypeReferenceComparer.Instance);
            Assert.IsType<CompositeParameterBuilder>(constMember.ParameterBuilder);
        }

        [Theory(DisplayName = "Проверка разрешения строкового параметра атрибута контракта"), AutoFixture]
        public void ParseIfParameterValueIsStringTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] Mock<IMethodParameterParser> methodParameterParserMock,
            ContractParametersResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("ConcreteClassWithParametersAttributes", "Method");
            var contractDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.Single(), methodDefinition, methodDefinition);

            var contractValidateParameters = sut.Resolve(contractDefinition, methodDefinition).ToList();

            methodParameterParserMock.Verify(mpp => mpp.Parse(methodDefinition, "value"), Times.Once);
            Assert.Single(contractValidateParameters, cvp => cvp.ParameterDefinition.Name == "y");
            Assert.Equal(2, contractValidateParameters.Count);
        }
    }
}
