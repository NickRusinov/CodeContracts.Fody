using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ContractInjectorTests
    {
        [Theory(DisplayName = "Проверка вызова получателя методов валидации контрактов при внедрении контрактов"), AutoFixture]
        public void ContractValidatesResolverHasBeenCalledTest(
            [Frozen] Mock<IContractValidatesResolver> contractValidatesResolverMock,
            ContractDefinition contractDefinition,
            MethodDefinition methodDefinition,
            ContractInjector sut)
        {
            sut.Inject(contractDefinition, methodDefinition);

            contractValidatesResolverMock.Verify(cvr => cvr.Resolve(contractDefinition, methodDefinition), Times.Once);
        }

        [Theory(DisplayName = "Проверка вызова получателя команд для контрактов при внедрении контрактов"), AutoFixture]
        public void InstructionsBuilderHasBeenCalledTest(
            [Frozen] Mock<IInstructionsBuilder> instructionsBuilderMock,
            ContractDefinition contractDefinition,
            MethodDefinition methodDefinition,
            ContractInjector sut)
        {
            sut.Inject(contractDefinition, methodDefinition);

            instructionsBuilderMock.Verify(ib => ib.Build(It.IsAny<ContractValidate>()), Times.Exactly(3));
        }
    }
}
