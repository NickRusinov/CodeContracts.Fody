using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractParameterBuilders
{
    public class NullParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка команд построителя пустого ссылочного параметра"), AutoFixture]
        public void LastLdnullInstructionTest(
            ParameterDefinition validateParameterDefinition,
            NullParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(validateParameterDefinition);

            Assert.Equal(Instruction.Create(OpCodes.Ldnull), buildedInstructions.SingleOrDefault(), InstructionComparer.Instance);
        }
    }
}
