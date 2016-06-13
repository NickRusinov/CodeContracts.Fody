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
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ContractMethodBuilderTests
    {
        [Theory(DisplayName = "Проверка первой команды построителя вызова контрактного метода"), AutoFixture]
        public void FirstNopTest(
            ContractValidate contractValidate,
            ContractMethodBuilder sut)
        {
            var buildedInstructions = sut.Build(contractValidate).ToList();

            Assert.Equal(Instruction.Create(OpCodes.Nop), buildedInstructions.FirstOrDefault(), InstructionComparer.Instance);
        }

        [Theory(DisplayName = "Проверка последней команды построителя вызова контрактного метода"), AutoFixture]
        public void LastCallMethodTest(
            [Frozen] MethodDefinition methodDefinition,
            ContractValidate contractValidate,
            ContractMethodBuilder sut)
        {
            var buildedInstructions = sut.Build(contractValidate).ToList();

            Assert.Equal(Instruction.Create(OpCodes.Call, methodDefinition), buildedInstructions.LastOrDefault(), InstructionComparer.Instance);
        }

        [Theory(DisplayName = "Проверка команд построителя вызова контрактного метода"), AutoFixture]
        public void ContainsInstructionsTest(
            [Frozen] Mock<IInstructionsBuilder> instructionsBuilderMock,
            ContractValidate contractValidate,
            ContractMethodBuilder sut)
        {
            var buildedInstructions = sut.Build(contractValidate).ToList();

            instructionsBuilderMock.Verify(ibm => ibm.Build(contractValidate), Times.Once);
            Assert.NotEmpty(buildedInstructions);
        }
    }
}
