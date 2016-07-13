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
            var typeDefinition = moduleDefinition.FindType("Jedi");

            var parseResult = sut.Parse(typeDefinition, "Name");

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.IsType<PropertyParameterBuilder>(parseResult.ParsedParameterBuilder);
            Assert.Equal(moduleDefinition.TypeSystem.String.Resolve(), parseResult.ParsedParameterType);
        }

        [Theory(DisplayName = "Проверка парсера параметров ссылок на свойство объекта")]
        [InlineAutoFixture("Name.Length", "Length")]
        [InlineAutoFixture("OrderRank.Length", "Length")]
        public void ComplexExistsPropertyTest(string parameterString, string tailParameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            PropertyParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("Jedi");

            var parseResult = sut.Parse(typeDefinition, parameterString);

            memberParameterParserMock.Verify(mpp => mpp.Parse(moduleDefinition.TypeSystem.String.Resolve(), tailParameterString));
            Assert.IsType<CompositeParameterBuilder>(parseResult.ParsedParameterBuilder);
        }

        [Theory(DisplayName = "Проверка парсера параметров ссылок на свойство объекта")]
        [InlineAutoFixture("Master")]
        [InlineAutoFixture("Master.OrderRank")]
        public void NotExistsPropertyTest(string parameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            PropertyParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("Jedi");

            var parseResult = sut.Parse(typeDefinition, parameterString);

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.Equal(ParseResult.Empty, parseResult);
        }
    }
}
