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
    public class ContractPropertyResolverTests
    {
        [Theory(DisplayName = "Проверка вызова парсера для строковых значений свойства атрибута контракта"), AutoFixture]
        public void ParseIfPropertyValueIsString(string anyParsedString,
            [Frozen]MethodDefinition methodDefinition,
            [Frozen]Mock<IMethodParameterParser> methodParameterParserMock,
            ContractPropertyResolver sut)
        {
            var contractPropertyDefinition = new ContractPropertyDefinition("not-used", anyParsedString);

            var parameterBuilder = sut.Resolve(contractPropertyDefinition, methodDefinition);

            methodParameterParserMock.Verify(mpp => mpp.Parse(methodDefinition, anyParsedString), Times.Once);
            Assert.IsType<CompositeParameterBuilder>(parameterBuilder);
        }

        [Theory(DisplayName = "Проверка вызова фабрики для нестроковых значений свойства атрибута контракта"), AutoFixture]
        public void ConstIfPropertyValueIsNotString(object anyConstObject,
            [Frozen]MethodDefinition methodDefinition,
            [Frozen]Mock<IConstParameterBuilderFactory> constParameterBuilderFactoryMock,
            ContractPropertyResolver sut)
        {
            var contractPropertyDefinition = new ContractPropertyDefinition("not-used", anyConstObject);

            var parameterBuilder = sut.Resolve(contractPropertyDefinition, methodDefinition);

            constParameterBuilderFactoryMock.Verify(cpbf => cpbf.Create(methodDefinition.Module, anyConstObject), Times.Once);
        }
    }
}
