using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractParameterBuilders;
using Mono.Cecil;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractParameterBuilders
{
    public class ConstParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка команд построителя значения константы")]
        [InlineAutoFixture(null, "ldnull")]
        [InlineAutoFixture((sbyte)5, "ldc.i4")]
        [InlineAutoFixture((short)5, "ldc.i4")]
        [InlineAutoFixture((int)5, "ldc.i4")]
        [InlineAutoFixture((long)5, "ldc.i8")]
        [InlineAutoFixture((byte)5, "ldc.i4")]
        [InlineAutoFixture((short)5, "ldc.i4")]
        [InlineAutoFixture((uint)5, "ldc.i4")]
        [InlineAutoFixture((ulong)5, "ldc.i8")]
        [InlineAutoFixture((float)5, "ldc.r4")]
        [InlineAutoFixture((double)5, "ldc.r8")]
        public void ConstNullParameterBuilderTest(object constParameter, string opcodeNameExpected,
            ParameterDefinition parameterDefinition)
        {
            var sut = new ConstParameterBuilder(constParameter);

            var buildedInstructions = sut.Build(parameterDefinition).ToList();

            Assert.Equal(opcodeNameExpected, buildedInstructions.Single().OpCode.Name);
        }
    }
}
