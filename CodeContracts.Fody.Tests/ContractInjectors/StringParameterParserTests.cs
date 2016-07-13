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
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class StringParameterParserTests
    {
        [Theory(DisplayName = "Проверка парсера параметра строки константы")]
        [InlineAutoFixture("the phantom menace")]
        [InlineAutoFixture("attack of the clones")]
        [InlineAutoFixture("revenge of the sith")]
        public void SimpleStringTest(string parameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            MethodDefinition methodDefinition,
            StringParameterParser sut)
        {
            var parseResult = sut.Parse(methodDefinition, parameterString);
            
            Assert.IsType<StringParameterBuilder>(parseResult.ParsedParameterBuilder);
            Assert.Equal(moduleDefinition.TypeSystem.String.Resolve(), parseResult.ParsedParameterType);
            Assert.Equal(parameterString, parseResult.ParsedParameterBuilder.FindPrivateField<string>("stringParameter"));
        }

        [Theory(DisplayName = "Проверка парсера параметра строки константы")]
        [InlineAutoFixture("$$a new hope", "$a new hope")]
        [InlineAutoFixture("$$the empire strikes back", "$the empire strikes back")]
        [InlineAutoFixture("$$return of the jedi", "$return of the jedi")]
        public void ComplexStringTest(string parameterString, string expectedParameterString,
            [Frozen]ModuleDefinition moduleDefinition,
            MethodDefinition methodDefinition,
            StringParameterParser sut)
        {
            var parseResult = sut.Parse(methodDefinition, parameterString);
            
            Assert.IsType<StringParameterBuilder>(parseResult.ParsedParameterBuilder);
            Assert.Equal(moduleDefinition.TypeSystem.String.Resolve(), parseResult.ParsedParameterType);
            Assert.Equal(expectedParameterString, parseResult.ParsedParameterBuilder.FindPrivateField<string>("stringParameter"));
        }
    }
}

