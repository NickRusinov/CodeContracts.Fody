using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.ContractParameterParsers;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractParameterParsers
{
    public class PropertyParameterParserTests
    {
        [Theory(DisplayName = "Проверка парсера параметров ссылок на свойство объекта"), AutoFixture]
        public void SimpleExistsPropertyTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            PropertyParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("ConcreteClassWithProperty");

            var parseResult = sut.Parse(typeDefinition, "Property");

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.IsType<PropertyParameterBuilder>(parseResult.ParsedParameterBuilder);
            Assert.Equal(moduleDefinition.FindType("Property"), parseResult.ParsedParameterType);
        }

        [Theory(DisplayName = "Проверка парсера параметров ссылок на свойство объекта")]
        [InlineAutoFixture("Property.Length", "Length")]
        public void ComplexExistsPropertyTest(string parameterString, string tailParameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            PropertyParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("ConcreteClassWithProperty");

            var parseResult = sut.Parse(typeDefinition, parameterString);

            memberParameterParserMock.Verify(mpp => mpp.Parse(moduleDefinition.FindType("Property"), tailParameterString));
            Assert.IsType<CompositeParameterBuilder>(parseResult.ParsedParameterBuilder);
        }

        [Theory(DisplayName = "Проверка парсера параметров ссылок на свойство объекта")]
        [InlineAutoFixture("NoProperty")]
        [InlineAutoFixture("NoProperty.OrderRank")]
        public void NotExistsPropertyTest(string parameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            PropertyParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("ConcreteClassWithProperty");

            var parseResult = sut.Parse(typeDefinition, parameterString);

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.Equal(ParseResult.Empty, parseResult);
        }
    }
}
