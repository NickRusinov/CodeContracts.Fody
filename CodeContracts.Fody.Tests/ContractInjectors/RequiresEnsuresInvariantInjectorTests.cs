using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractInstructionsBuilders;
using CodeContracts.Fody.ContractValidateResolvers;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class RequiresEnsuresInvariantInjectorTests
    {
        [Theory(DisplayName = "Проверка вызова получателя методов валидации контрактов при внедрении контрактов"), AutoFixture]
        public void ContractValidatesResolverHasBeenCalledTest(
            [Frozen] Mock<IContractValidateResolver> contractValidateResolverMock,
            ContractDefinition contractDefinition,
            MethodDefinition methodDefinition,
            RequiresEnsuresInvariantInjector sut)
        {
            sut.Inject(contractDefinition, methodDefinition);

            contractValidateResolverMock.Verify(cvr => cvr.Resolve(contractDefinition, methodDefinition), Times.Once);
        }

        [Theory(DisplayName = "Проверка вызова получателя команд для контрактов при внедрении контрактов"), AutoFixture]
        public void InstructionsBuilderHasBeenCalledTest(
            [Frozen] Mock<IInstructionsBuilder> instructionsBuilderMock,
            ContractDefinition contractDefinition,
            MethodDefinition methodDefinition,
            RequiresEnsuresInvariantInjector sut)
        {
            sut.Inject(contractDefinition, methodDefinition);

            instructionsBuilderMock.Verify(ib => ib.Build(It.IsAny<ContractValidate>()), Times.Once);
        }
    }
}
