﻿using System;
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
    public class ArgumentParameterParserTests
    {
        [Theory(DisplayName = "Проверка парсера аргументов методов для параметров, заданных строкой"), AutoFixture]
        public void SimpleExistsArgumentTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            ArgumentParameterParser sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("Jedi", "UseTheForce");

            var builders = sut.Parse(methodDefinition, "$force").ToList();

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.IsType<ArgumentParameterBuilder>(builders.First());
        }

        [Theory(DisplayName = "Проверка парсера аргументов методов для параметров, заданных строкой")]
        [InlineAutoFixture("$force.Feel.you", "Feel.you")]
        [InlineAutoFixture("$force.Use.you", "Use.you")]
        public void ComplexExistsArgumentTest(string parameterString, string tailParameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            ArgumentParameterParser sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("Jedi", "UseTheForce");

            var builders = sut.Parse(methodDefinition, parameterString).ToList();

            memberParameterParserMock.Verify(mpp => mpp.Parse(moduleDefinition.FindType("Force"), tailParameterString));
            Assert.IsType<ArgumentParameterBuilder>(builders.First());
        }

        [Theory(DisplayName = "Проверка парсера аргументов методов для параметров, заданных строкой")]
        [InlineAutoFixture("$lightsaber")]
        [InlineAutoFixture("$lightsaber.Use.you")]
        public void NotExistsArgumentTest(string parameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMemberParameterParser> memberParameterParserMock,
            ArgumentParameterParser sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("Jedi", "UseTheForce");

            var builders = sut.Parse(methodDefinition, parameterString).ToList();

            memberParameterParserMock.Verify(mpp => mpp.Parse(It.IsAny<TypeDefinition>(), It.IsAny<string>()), Times.Never);
            Assert.Empty(builders);
        }
    }
}