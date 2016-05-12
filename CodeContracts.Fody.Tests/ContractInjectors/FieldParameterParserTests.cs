using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class FieldParameterParserTests
    {
        [Theory(DisplayName = "Проверка парсера параметров ссылок на поле объекта"), AutoFixture]
        public void SimpleExistsFieldTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            FieldParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("DarthMaul");

            var builders = sut.Parse(typeDefinition, "lightsaber").ToList();

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.IsType<FieldParameterBuilder>(builders.First());
        }

        [Theory(DisplayName = "Проверка парсера параметров ссылок на поле объекта")]
        [InlineAutoFixture("lightsaber.Color", "Color")]
        [InlineAutoFixture("lightsaber.BladeCount", "BladeCount")]
        public void ComplexExistsFieldTest(string parameterString, string tailParameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            FieldParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("DarthMaul");

            var builders = sut.Parse(typeDefinition, parameterString).ToList();

            memberParameterParserMock.Verify(mpp => mpp.Parse(moduleDefinition.FindType("Lightsaber"), tailParameterString));
            Assert.IsType<FieldParameterBuilder>(builders.First());
        }

        [Theory(DisplayName = "Проверка парсера параметров ссылок на поле объекта")]
        [InlineAutoFixture("heart")]
        [InlineAutoFixture("soul.IsAlive")]
        public void NotExistsFieldTest(string parameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            FieldParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("DarthMaul");

            var builders = sut.Parse(typeDefinition, parameterString).ToList();

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.Empty(builders);
        }
    }
}
