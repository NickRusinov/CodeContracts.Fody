using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractScanners;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractScanners
{
    public class PropertyScannerTests
    {
        [Theory(DisplayName = "Проверка получения атрибута контракта постусловия для метода задания свойства"), AutoFixture]
        public void ScanContractSetEnsuresTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            PropertyScanner sut)
        {
            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(true);

            var contracts = sut.Scan(moduleDefinition.FindProperty("ConcreteClassWithProperty", "PropertyWithAttribute")).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Single(contracts.OfType<EnsuresDefinition>());
        }

        [Theory(DisplayName = "Проверка отсутствия атрибута контракта постусловия для метода задания свойства"), AutoFixture]
        public void ScanNoContractSetEnsuresTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            PropertyScanner sut)
        {
            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(false);

            var contracts = sut.Scan(moduleDefinition.FindProperty("ConcreteClassWithProperty", "PropertyWithAttribute")).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Empty(contracts);
        }

        [Theory(DisplayName = "Проверка получения атрибута контракта предусловия для метода получения свойства"), AutoFixture]
        public void ScanContractGetRequiresTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            PropertyScanner sut)
        {
            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(true);

            var contracts = sut.Scan(moduleDefinition.FindProperty("ConcreteClassWithProperty", "PropertyWithAttribute")).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Single(contracts.OfType<RequiresDefinition>());
        }

        [Theory(DisplayName = "Проверка отсутствия атрибута контракта предусловия для метода получения свойства"), AutoFixture]
        public void ScanNoContractGetRequiresTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            PropertyScanner sut)
        {
            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(false);

            var contracts = sut.Scan(moduleDefinition.FindProperty("ConcreteClassWithProperty", "PropertyWithAttribute")).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Empty(contracts);
        }
    }
}
