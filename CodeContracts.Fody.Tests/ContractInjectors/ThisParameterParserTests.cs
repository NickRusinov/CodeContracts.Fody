using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ThisParameterParserTests
    {
        [Theory(DisplayName = "Проверка парсера параметра ссылки на текущий объект"), AutoFixture]
        public void SimpleThisTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            ThisParameterParser sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("Jedi", "UseTheForce");

            var parseResult = sut.Parse(methodDefinition, "$");

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.IsType<ThisParameterBuilder>(parseResult.ParsedParameterBuilder);
            Assert.Equal(moduleDefinition.FindType("Jedi"), parseResult.ParsedParameterType);
        }

        [Theory(DisplayName = "Проверка парсера параметра ссылки на текущий объект")]
        [InlineAutoFixture("$.Feel.you", "Feel.you")]
        [InlineAutoFixture("$.Use.you", "Use.you")]
        public void ComplexThisTest(string parameterString, string tailParameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            ThisParameterParser sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("Jedi", "UseTheForce");

            var parseResult = sut.Parse(methodDefinition, parameterString);

            memberParameterParserMock.Verify(mpp => mpp.Parse(moduleDefinition.FindType("Jedi"), tailParameterString));
            Assert.IsType<CompositeParameterBuilder>(parseResult.ParsedParameterBuilder);
        }
    }
}
