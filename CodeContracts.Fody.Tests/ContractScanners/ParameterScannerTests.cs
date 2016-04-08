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
    public class ParameterScannerTests
    {
        [Theory(DisplayName = "Проверка получения атрибута контракта для параметра"), AutoFixture]
        public void ScanContractParameterTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            ParameterScanner sut)
        {
            var parameter = moduleDefinition.GetType(Constants.SithNamespace, "DarthMaul")
                .Methods.Single(md => md.Name == "KillJedi")
                .Parameters.Single(pd => pd.Name == "jedi");

            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(true);

            var contracts = sut.Scan(parameter).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Single(contracts);
            Assert.IsAssignableFrom<RequiresDefinition>(contracts.Single());
        }

        [Theory(DisplayName = "Проверка отсутствия атрибута контракта для параметра"), AutoFixture]
        public void ScanNoContractParameterTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            ParameterScanner sut)
        {
            var parameter = moduleDefinition.GetType(Constants.SithNamespace, "DarthMaul")
                .Methods.Single(md => md.Name == "KillJedi")
                .Parameters.Single(pd => pd.Name == "revenge");

            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(false);

            var contracts = sut.Scan(parameter).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Empty(contracts);
        }
    }
}
