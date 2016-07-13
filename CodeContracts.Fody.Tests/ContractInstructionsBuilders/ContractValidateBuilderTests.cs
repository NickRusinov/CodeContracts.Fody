using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractInstructionsBuilders;
using CodeContracts.Fody.ContractParameterBuilders;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInstructionsBuilders
{
    public class ContractValidateBuilderTests
    {
        [Theory(DisplayName = "Проверка первой команды построителя вызова метода валидации для контракта"), AutoFixture]
        public void FirstNopTest(
            ContractValidate contractValidate,
            ContractValidateBuilder sut)
        {
            var buildedInstructions = sut.Build(contractValidate).ToList();

            Assert.Equal(Instruction.Create(OpCodes.Nop), buildedInstructions.FirstOrDefault(), InstructionComparer.Instance);
        }

        [Theory(DisplayName = "Проверка последней команды построителя вызова метода валидации для контракта"), AutoFixture]
        public void LastCallMethodTest(
            ContractValidate contractValidate,
            ContractValidateBuilder sut)
        {
            var buildedInstructions = sut.Build(contractValidate).ToList();

            Assert.Equal(Instruction.Create(OpCodes.Call, contractValidate.ValidateDefinition.ValidateMethod), buildedInstructions.LastOrDefault(), InstructionComparer.Instance);
        }

        [Theory(DisplayName = "Проверка команд построителя вызова метода валидации для контракта"), AutoFixture]
        public void ContainsInstructionsTest(
            [Frozen] Mock<IParameterBuilder> parameterBuilderMock,
            ContractValidate contractValidate,
            ContractValidateBuilder sut)
        {
            var buildedInstructions = sut.Build(contractValidate).ToList();

            parameterBuilderMock.Verify(pbm => pbm.Build(It.IsAny<ParameterDefinition>()), Times.Exactly(contractValidate.ValidateDefinition.ValidateMethod.Parameters.Count));
            Assert.NotEmpty(buildedInstructions);
        }
    }
}
