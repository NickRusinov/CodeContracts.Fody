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
    public class MethodScannerTests
    {
        [Theory(DisplayName = "Проверка сканирования всех параметров метода"), AutoFixture]
        public void ScanAllParametersTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IParameterScanner> parameterScannerMock,
            MethodScanner sut)
        {
            sut.Scan(moduleDefinition.FindMethod("DarthMaul", "KillJedi")).ToList();

            parameterScannerMock.Verify(ps => ps.Scan(It.IsAny<ParameterDefinition>()), Times.Exactly(2));
        }

        [Theory(DisplayName = "Проверка сканирования возвращаемого значения метода"), AutoFixture]
        public void ScanAllMethodReturnTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMethodReturnScanner> methodReturnScannerMock,
            MethodScanner sut)
        {
            sut.Scan(moduleDefinition.FindMethod("DarthMaul", "KillJedi")).ToList();

            methodReturnScannerMock.Verify(mrs => mrs.Scan(It.IsAny<MethodReturnType>()), Times.Once);
        }

        [Theory(DisplayName = "Проверка получения атрибута контракта для метода"), AutoFixture]
        public void ScanContractMethodTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            MethodScanner sut)
        {
            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(true);

            var contracts = sut.Scan(moduleDefinition.FindMethod("DarthMaul", "JoinDarkSide")).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Single(contracts.OfType<RequiresDefinition>());
        }

        [Theory(DisplayName = "Проверка отсутствия атрибута контракта для метода"), AutoFixture]
        public void ScanNoContractMethodTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            MethodScanner sut)
        {
            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(false);

            var contracts = sut.Scan(moduleDefinition.FindMethod("DarthMaul", "KillJedi")).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Empty(contracts.OfType<RequiresDefinition>());
        }
    }
}
