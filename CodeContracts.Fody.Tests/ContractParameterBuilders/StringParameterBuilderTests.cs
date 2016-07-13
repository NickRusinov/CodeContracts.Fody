using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractParameterBuilders
{
    public class StringParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка команд построителя параметра строки константы"), AutoFixture]
        public void LastLdstrInstructionTest(
            [Frozen]string stringParameter,
            ParameterDefinition validateParameterDefinition,
            StringParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(validateParameterDefinition);

            Assert.Equal(Instruction.Create(OpCodes.Ldstr, stringParameter), buildedInstructions.SingleOrDefault(), InstructionComparer.Instance);
        }
    }
}
