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
    public class ArgumentParameterParserTests
    {
        [Theory(DisplayName = "Проверка парсера аргументов методов для параметров, заданных строкой"), AutoFixture]
        public void SimpleExistsArgumentTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            ArgumentParameterParser sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("ConcreteClass", "MethodWithParameter");

            var parseResult = sut.Parse(methodDefinition, "$parameter");

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.IsType<ArgumentParameterBuilder>(parseResult.ParsedParameterBuilder);
            Assert.Equal(moduleDefinition.FindType("Parameter"), parseResult.ParsedParameterType);
        }

        [Theory(DisplayName = "Проверка парсера аргументов методов для параметров, заданных строкой")]
        [InlineAutoFixture("$parameter.Feel.you", "Feel.you")]
        [InlineAutoFixture("$parameter.Use.you", "Use.you")]
        public void ComplexExistsArgumentTest(string parameterString, string tailParameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            ArgumentParameterParser sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("ConcreteClass", "MethodWithParameter");

            var parseResult = sut.Parse(methodDefinition, parameterString);

            memberParameterParserMock.Verify(mpp => mpp.Parse(moduleDefinition.FindType("Parameter"), tailParameterString));
            Assert.IsType<CompositeParameterBuilder>(parseResult.ParsedParameterBuilder);
        }

        [Theory(DisplayName = "Проверка парсера аргументов методов для параметров, заданных строкой")]
        [InlineAutoFixture("$noparameter")]
        [InlineAutoFixture("$noparameter.Use.you")]
        public void NotExistsArgumentTest(string parameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            ArgumentParameterParser sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("ConcreteClass", "MethodWithParameter");

            var parseResult = sut.Parse(methodDefinition, parameterString);

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.Equal(ParseResult.Empty, parseResult);
        }
    }
}
