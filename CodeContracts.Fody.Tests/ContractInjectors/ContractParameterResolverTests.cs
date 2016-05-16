using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ContractParameterResolverTests
    {
        [Theory(DisplayName = "Проверка вызова парсера для строковых значений параметра атрибута контракта"), AutoFixture]
        public void ParseIfParameterValueIsString(string anyParsedString,
            [Frozen]MethodDefinition methodDefinition,
            [Frozen]Mock<IMethodParameterParser> methodParameterParserMock,
            ContractParameterResolver sut)
        {
            var contractParameterDefinition = new ContractParameterDefinition("not-used", anyParsedString);

            var parameterBuilder = sut.Resolve(contractParameterDefinition, methodDefinition);

            methodParameterParserMock.Verify(mpp => mpp.Parse(methodDefinition, anyParsedString), Times.Once);
            Assert.IsType<CompositeParameterBuilder>(parameterBuilder);
        }

        [Theory(DisplayName = "Проверка вызова фабрики для нестроковых значений параметра атрибута контракта"), AutoFixture]
        public void ConstIfParameterValueIsNotString(object anyConstObject,
            [Frozen]MethodDefinition methodDefinition,
            [Frozen]Mock<IConstParameterBuilderFactory> constParameterBuilderFactoryMock,
            ContractParameterResolver sut)
        {
            var contractParameterDefinition = new ContractParameterDefinition("not-used", anyConstObject);

            var parameterBuilder = sut.Resolve(contractParameterDefinition, methodDefinition);

            constParameterBuilderFactoryMock.Verify(cpbf => cpbf.Create(methodDefinition.Module, anyConstObject), Times.Once);
        }
    }
}
