using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractInstructionsBuilders;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInstructionsBuilders
{
    public class ContractMethodWithMessageBuilderTests
    {
        [Theory(DisplayName = "Проверка первой команды построителя вызова контрактного метода с сообщением"), AutoFixture]
        public void FirstNopTest(
            ContractValidate contractValidate,
            ContractMethodWithMessageBuilder sut)
        {
            var buildedInstructions = sut.Build(contractValidate).ToList();

            Assert.Equal(Instruction.Create(OpCodes.Nop), buildedInstructions.FirstOrDefault(), InstructionComparer.Instance);
        }

        [Theory(DisplayName = "Проверка команды загрузки строки построителя вызова контрактного метода с сообщением"), AutoFixture]
        public void PreLastLoadStringTest(
            [Frozen]string message,
            ContractValidate contractValidate,
            ContractMethodWithMessageBuilder sut)
        {
            var buildedInstructions = sut.Build(contractValidate).ToList() as IEnumerable<Instruction>;

            Assert.Equal(Instruction.Create(OpCodes.Ldstr, message), buildedInstructions.Reverse().Skip(1).FirstOrDefault(), InstructionComparer.Instance);
        }

        [Theory(DisplayName = "Проверка последней команды построителя вызова контрактного метода с сообщением"), AutoFixture]
        public void LastCallMethodTest(
            [Frozen]MethodDefinition methodDefinition,
            ContractValidate contractValidate,
            ContractMethodWithMessageBuilder sut)
        {
            var buildedInstructions = sut.Build(contractValidate).ToList();

            Assert.Equal(Instruction.Create(OpCodes.Call, methodDefinition), buildedInstructions.LastOrDefault(), InstructionComparer.Instance);
        }

        [Theory(DisplayName = "Проверка команд построителя вызова контрактного метода с сообщением"), AutoFixture]
        public void ContainsInstructionsTest(
            [Frozen] Mock<IInstructionsBuilder> instructionsBuilderMock,
            ContractValidate contractValidate,
            ContractMethodWithMessageBuilder sut)
        {
            var buildedInstructions = sut.Build(contractValidate).ToList();

            instructionsBuilderMock.Verify(ibm => ibm.Build(contractValidate), Times.Once);
            Assert.NotEmpty(buildedInstructions);
        }
    }
}
