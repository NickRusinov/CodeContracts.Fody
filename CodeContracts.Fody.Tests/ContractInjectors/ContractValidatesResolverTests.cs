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
    public class ContractValidatesResolverTests
    {
        [Theory(DisplayName = "Проверка вызова получателя валидатора контракта"), AutoFixture]
        public void ContractValidateResolverHasBeenCalledTest(
            [Frozen] Mock<IContractValidateResolver> contractValidateResolverMock,
            ContractDefinition contractDefinition,
            MethodDefinition methodDefinition,
            ContractValidatesResolver sut)
        {
            var contractValidates = sut.Resolve(contractDefinition, methodDefinition).ToList();

            contractValidateResolverMock.Verify(cvr => cvr.Resolve(contractDefinition, It.Is<IReadOnlyCollection<ContractValidateParameter>>(cms => cms.Count == 10)), Times.Exactly(3));
            Assert.Equal(3, contractValidates.Count);
        }

        [Theory(DisplayName = "Проверка вызова получателя параметров для валидатора контракта"), AutoFixture]
        public void ContractValidateParametersResolverHasBeenCalledTest(
            [Frozen] Mock<IContractValidateParametersResolver> contractValidateParametersResolverMock,
            ContractDefinition contractDefinition,
            MethodDefinition methodDefinition,
            ContractValidatesResolver sut)
        {
            var contractValidates = sut.Resolve(contractDefinition, methodDefinition).ToList();

            contractValidateParametersResolverMock.Verify(cvr => cvr.Resolve(contractDefinition, methodDefinition), Times.Exactly(4));
            Assert.Equal(3, contractValidates.Count);
        }
    }
}
