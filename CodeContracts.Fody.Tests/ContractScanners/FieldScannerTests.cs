﻿using System;
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
    public class FieldScannerTests
    {
        [Theory(DisplayName = "Проверка получения атрибута контракта для поля"), AutoFixture]
        public void ScanContractFieldTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            FieldScanner sut)
        {
            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(true);

            var contracts = sut.Scan(moduleDefinition.FindField("DarthMaul", "lightsaber")).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Single(contracts);
            Assert.IsAssignableFrom<InvariantDefinition>(contracts.Single());
        }

        [Theory(DisplayName = "Проверка отсутствия атрибута контракта для поля"), AutoFixture]
        public void ScanNoContractFieldTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            FieldScanner sut)
        {
            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(false);

            var contracts = sut.Scan(moduleDefinition.FindField("DarthMaul", "master")).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Empty(contracts);
        }
    }
}
