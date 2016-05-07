using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ArgumentParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка первой команды построителя параметра ссылки на аргумент (параметр) метода"), AutoFixture]
        public void FirstNopInstructionTest(
            IEnumerable<Instruction> instructions,
            ArgumentParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Equal(Instruction.Create(OpCodes.Nop), buildedInstructions.FirstOrDefault(), InstructionComparer.Default);
        }

        [Theory(DisplayName = "Проверка последней команды построителя параметра ссылки на аргумент (параметр) метода"), AutoFixture]
        public void LastLdargInstructionTest(
            [Frozen]ParameterDefinition parameterDefinition,
            IEnumerable<Instruction> instructions,
            ArgumentParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Equal(Instruction.Create(OpCodes.Ldarg, parameterDefinition), buildedInstructions.LastOrDefault(), InstructionComparer.Default);
        }

        [Theory(DisplayName = "Проверка команд построителя параметра ссылки на аргумент (параметр) метода"), AutoFixture]
        public void ContainsInstructionsTest(
            IEnumerable<Instruction> instructions,
            ArgumentParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Empty(instructions.Except(buildedInstructions, InstructionComparer.Default));
        }
    }
}
