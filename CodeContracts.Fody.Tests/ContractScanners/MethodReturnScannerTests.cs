using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractScanners;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractScanners
{
    public class MethodReturnScannerTests
    {
        [Theory(DisplayName = "Проверка получения атрибута контракта для возвращаемого значения"), AutoFixture]
        public void ScanContractMethodReturnTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            MethodReturnScanner sut)
        {
            var methodReturn = moduleDefinition.GetType(Constants.SithNamespace, "DarthMaul")
                .Methods.Single(md => md.Name == "KillJedi").MethodReturnType;

            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(true);

            var contracts = sut.Scan(methodReturn).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Single(contracts);
            Assert.IsAssignableFrom<EnsuresDefinition>(contracts.Single());
        }

        [Theory(DisplayName = "Проверка отсутствия атрибута контракта для возвращаемого значения"), AutoFixture]
        public void ScanNoContractMethodReturnTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            MethodReturnScanner sut)
        {
            var methodReturn = moduleDefinition.GetType(Constants.SithNamespace, "DarthMaul")
                .Methods.Single(md => md.Name == "JoinDarkSide").MethodReturnType;

            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(false);

            var contracts = sut.Scan(methodReturn).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Empty(contracts);
        }
    }
}
