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
        [Theory(DisplayName = "Проверка команд построителя параметра ссылки на аргумент (параметр) метода"), AutoFixture]
        public void LastLdargInstructionTest(
            [Frozen]ParameterDefinition parameterDefinition,
            ParameterDefinition validateParameterDefinition,
            ArgumentParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(validateParameterDefinition);

            Assert.Equal(Instruction.Create(OpCodes.Ldarg, parameterDefinition), buildedInstructions.SingleOrDefault(), InstructionComparer.Instance);
        }
    }
}
