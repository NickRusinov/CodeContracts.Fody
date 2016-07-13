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
    public class ThisParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка команд построителя параметра ссылки на текущий экземпляр"), AutoFixture]
        public void LastLdargInstructionTest(
            ParameterDefinition validateParameterDefinition,
            ThisParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(validateParameterDefinition);

            Assert.Equal(Instruction.Create(OpCodes.Ldarg_0), buildedInstructions.SingleOrDefault(), InstructionComparer.Instance);
        }
    }
}
