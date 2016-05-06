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
    public class ContractValidateBuilderTests
    {
        [Theory(DisplayName = "Проверка первой команды построителя вызова метода валидации для контракта"), AutoFixture]
        public void FirstNopTest(
            [Frozen]MethodDefinition methodDefinition,
            IEnumerable<Instruction> instructions,
            ContractValidateBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Equal(Instruction.Create(OpCodes.Nop), buildedInstructions.FirstOrDefault(), InstructionComparer.Default);
        }

        [Theory(DisplayName = "Проверка последней команды построителя вызова метода валидации для контракта"), AutoFixture]
        public void LastCallMethodTest(
            [Frozen]MethodDefinition methodDefinition,
            IEnumerable<Instruction> instructions,
            ContractValidateBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Equal(Instruction.Create(OpCodes.Call, methodDefinition), buildedInstructions.LastOrDefault(), InstructionComparer.Default);
        }

        [Theory(DisplayName = "Проверка команд построителя вызова метода валидации для контракта"), AutoFixture]
        public void ContainsInstructionsTest(
            [Frozen]MethodDefinition methodDefinition,
            IEnumerable<Instruction> instructions,
            ContractValidateBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Empty(instructions.Except(buildedInstructions, InstructionComparer.Default));
        }
    }
}
