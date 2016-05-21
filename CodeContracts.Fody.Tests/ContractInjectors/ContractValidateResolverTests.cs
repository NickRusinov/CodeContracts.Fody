using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ContractValidateResolverTests
    {
        [Theory(DisplayName = "Проверка вызова сканера методов валидации контрактов"), AutoFixture]
        public void ContractValidateScannerHasBeenCalledTest(
            [Frozen] Mock<IContractValidateScanner> contractValidateScannerMock,
            CustomAttribute customAttribute,
            ICollection<ContractMember> contractMembers,
            ContractValidateResolver sut)
        {
            sut.Resolve(customAttribute, contractMembers);

            contractValidateScannerMock.Verify(cvs => cvs.Scan(customAttribute), Times.Once);
        }

        [Theory(DisplayName = "Проверка вызова получателя наилучшей перегрузки методов валидации контрактов"), AutoFixture]
        public void BestOverloadResolverHasBeenCalledTest(
            [Frozen] Mock<IBestOverloadResolver> bestOverloadResolverMock,
            CustomAttribute customAttribute,
            ICollection<ContractMember> contractMembers,
            ContractValidateResolver sut)
        {
            sut.Resolve(customAttribute, contractMembers);

            bestOverloadResolverMock.Verify(bor => bor.Resolve(It.IsAny<IEnumerable<MethodDefinition>>(), It.IsAny<IEnumerable<ParameterDefinition>>()), Times.Once);
        }
    }
}
