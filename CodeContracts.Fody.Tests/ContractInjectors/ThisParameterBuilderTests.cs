﻿using System;
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
    public class ThisParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка первой команды построителя параметра ссылки на текущий экземпляр"), AutoFixture]
        public void FirstNopInstructionTest(
            IEnumerable<Instruction> instructions,
            ThisParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Equal(Instruction.Create(OpCodes.Nop), buildedInstructions.FirstOrDefault(), InstructionComparer.Default);
        }

        [Theory(DisplayName = "Проверка последней команды построителя параметра ссылки на текущий экземпляр"), AutoFixture]
        public void LastLdargInstructionTest(
            IEnumerable<Instruction> instructions,
            ThisParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Equal(Instruction.Create(OpCodes.Ldarg_0), buildedInstructions.LastOrDefault(), InstructionComparer.Default);
        }

        [Theory(DisplayName = "Проверка команд построителя параметра ссылки на текущий экземпляр"), AutoFixture]
        public void ContainsInstructionsTest(
            IEnumerable<Instruction> instructions,
            ThisParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(instructions);

            Assert.Empty(instructions.Except(buildedInstructions, InstructionComparer.Default));
        }
    }
}
