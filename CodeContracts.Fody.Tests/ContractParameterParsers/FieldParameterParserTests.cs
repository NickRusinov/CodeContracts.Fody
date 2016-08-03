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
    public class FieldParameterParserTests
    {
        [Theory(DisplayName = "Проверка парсера параметров ссылок на поле объекта"), AutoFixture]
        public void SimpleExistsFieldTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            FieldParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("ConcreteClassWithField");

            var parseResult = sut.Parse(typeDefinition, "field");

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.IsType<FieldParameterBuilder>(parseResult.ParsedParameterBuilder);
            Assert.Equal(moduleDefinition.FindType("Field"), parseResult.ParsedParameterType);
        }

        [Theory(DisplayName = "Проверка парсера параметров ссылок на поле объекта")]
        [InlineAutoFixture("field.Color", "Color")]
        [InlineAutoFixture("field.BladeCount", "BladeCount")]
        public void ComplexExistsFieldTest(string parameterString, string tailParameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            FieldParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("ConcreteClassWithField");

            var parseResult = sut.Parse(typeDefinition, parameterString);

            memberParameterParserMock.Verify(mpp => mpp.Parse(moduleDefinition.FindType("Field"), tailParameterString));
            Assert.IsType<CompositeParameterBuilder>(parseResult.ParsedParameterBuilder);
        }

        [Theory(DisplayName = "Проверка парсера параметров ссылок на поле объекта")]
        [InlineAutoFixture("nofield")]
        [InlineAutoFixture("nofield.IsAlive")]
        public void NotExistsFieldTest(string parameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            FieldParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("ConcreteClassWithField");

            var parseResult = sut.Parse(typeDefinition, parameterString);

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.Equal(ParseResult.Empty, parseResult);
        }
    }
}
