using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil.Cil;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class NullParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка первой команды построителя пустого ссылочного параметра"), AutoFixture]
        public void FirstNopInstructionTest(
            IEnumerable<Instruction> instructions,
            NullParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Equal(Instruction.Create(OpCodes.Nop), buildedInstructions.FirstOrDefault(), InstructionComparer.Default);
        }

        [Theory(DisplayName = "Проверка последней команды построителя пустого ссылочного параметра"), AutoFixture]
        public void LastLdnullInstructionTest(
            IEnumerable<Instruction> instructions,
            NullParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Equal(Instruction.Create(OpCodes.Ldnull), buildedInstructions.LastOrDefault(), InstructionComparer.Default);
        }

        [Theory(DisplayName = "Проверка команд построителя пустого ссылочного параметра"), AutoFixture]
        public void ContainsInstructionsTest(
            IEnumerable<Instruction> instructions,
            NullParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Empty(instructions.Except(buildedInstructions, InstructionComparer.Default));
        }
    }
}
