using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
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
            var builders = sut.Parse(methodDefinition, parameterString).ToList();
            
            Assert.IsType<StringParameterBuilder>(builders.Single());
            Assert.Equal(parameterString, builders.Single().FindPrivateField<string>("stringParameter"));
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
            var builders = sut.Parse(methodDefinition, parameterString).ToList();
            
            Assert.IsType<StringParameterBuilder>(builders.Single());
            Assert.Equal(expectedParameterString, builders.Single().FindPrivateField<string>("stringParameter"));
        }
    }
}
