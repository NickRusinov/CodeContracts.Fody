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
    public class PropertyParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка команд построителя параметра ссылки на свойство"), AutoFixture]
        public void LastCallInstructionTest(
            [Frozen]PropertyDefinition propertyDefinition,
            ParameterDefinition validateParameterDefinition,
            PropertyParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(validateParameterDefinition);

            Assert.Equal(Instruction.Create(OpCodes.Callvirt, propertyDefinition.GetMethod), buildedInstructions.SingleOrDefault(), InstructionComparer.Instance);
        }
    }
}
