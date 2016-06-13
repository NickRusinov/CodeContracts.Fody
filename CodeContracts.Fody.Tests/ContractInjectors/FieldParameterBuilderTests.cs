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
    public class FieldParameterBuilderTests
    {
        [Theory(DisplayName = "Проверка команд построителя параметра ссылки на поле класса"), AutoFixture]
        public void LastLdfldInstructionTest(
            [Frozen]FieldDefinition fieldDefinition,
            ParameterDefinition validateParameterDefinition,
            FieldParameterBuilder sut)
        {
            var buildedInstructions = sut.Build(validateParameterDefinition);

            Assert.Equal(Instruction.Create(OpCodes.Ldfld, fieldDefinition), buildedInstructions.SingleOrDefault(), InstructionComparer.Instance);
        }
    }
}
