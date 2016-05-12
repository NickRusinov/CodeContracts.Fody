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
    public class PropertyParameterParserTests
    {
        [Theory(DisplayName = "Проверка парсера параметров ссылок на свойство объекта"), AutoFixture]
        public void SimpleExistsPropertyTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            PropertyParameterParser sut)
        {
            var typeDefinition = moduleDefinition.FindType("Jedi");

            var builders = sut.Parse(typeDefinition, "Name").ToList();

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.IsType<PropertyParameterBuilder>(builders.First());
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

            var builders = sut.Parse(typeDefinition, parameterString).ToList();

            memberParameterParserMock.Verify(mpp => mpp.Parse(moduleDefinition.TypeSystem.String.Resolve(), tailParameterString));
            Assert.IsType<PropertyParameterBuilder>(builders.First());
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

            var builders = sut.Parse(typeDefinition, parameterString).ToList();

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.Empty(builders);
        }
    }
}
