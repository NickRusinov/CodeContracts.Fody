using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil.Cil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class StringParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка первой команды построителя параметра строки константы"), AutoFixture]
        public void FirstNopInstructionTest(
            IEnumerable<Instruction> instructions,
            StringParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Equal(Instruction.Create(OpCodes.Nop), buildedInstructions.FirstOrDefault(), InstructionComparer.Default);
        }

        [Theory(DisplayName = "Проверка последней команды построителя параметра строки константы"), AutoFixture]
        public void LastLdstrInstructionTest(
            [Frozen]string stringParameter,
            IEnumerable<Instruction> instructions,
            StringParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Equal(Instruction.Create(OpCodes.Ldstr, stringParameter), buildedInstructions.LastOrDefault(), InstructionComparer.Default);
        }

        [Theory(DisplayName = "Проверка команд построителя параметра строки константы"), AutoFixture]
        public void ContainsInstructionsTest(
            IEnumerable<Instruction> instructions,
            StringParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Empty(instructions.Except(buildedInstructions, InstructionComparer.Default));
        }
    }
}
