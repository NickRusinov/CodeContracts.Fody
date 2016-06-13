using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ConvertParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка команд построителя команд преобразования примитивных типов")]
        [InlineAutoFixture(typeof(sbyte), "conv.i1")]
        [InlineAutoFixture(typeof(short), "conv.i2")]
        [InlineAutoFixture(typeof(int), "conv.i4")]
        [InlineAutoFixture(typeof(long), "conv.i8")]
        [InlineAutoFixture(typeof(byte), "conv.u1")]
        [InlineAutoFixture(typeof(ushort), "conv.u2")]
        [InlineAutoFixture(typeof(uint), "conv.u4")]
        [InlineAutoFixture(typeof(ulong), "conv.u8")]
        [InlineAutoFixture(typeof(float), "conv.r4")]
        [InlineAutoFixture(typeof(double), "conv.r8")]
        public void ConvertParameterBuilderTest(Type parameterType, string opcodeNameExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            ConvertParameterBuilder sut)
        {
            var parameterDefinition = new ParameterDefinition(moduleDefinition.ImportReference(parameterType));

            var buildedInstructions = sut.Build(parameterDefinition).ToList();

            Assert.Equal(opcodeNameExpected, buildedInstructions.Single().OpCode.Name);
        }
    }
}
