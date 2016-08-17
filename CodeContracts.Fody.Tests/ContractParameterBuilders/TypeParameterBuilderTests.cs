using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractParameterBuilders
{
    public class TypeParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка команд построителя параметра типа константы"), AutoFixture]
        public void FirstLadtokenInstructionTest(
            [Frozen] TypeReference typeReference,
            ParameterDefinition validateParameterDefinition,
            TypeParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(validateParameterDefinition).ToList();

            Assert.Equal(Instruction.Create(OpCodes.Ldtoken, typeReference), buildedInstructions[0], InstructionComparer.Instance);
        }

        [Theory(DisplayName = "Проверка команд построителя параметра типа константы"), AutoFixture]
        public void SecondCallInstructionTest(
            ParameterDefinition validateParameterDefinition,
            TypeParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(validateParameterDefinition).ToList();

            Assert.Equal(OpCodes.Call, buildedInstructions[1].OpCode);
            Assert.Equal("GetTypeFromHandle", ((MethodReference)buildedInstructions[1].Operand).Name);
        }
    }
}
