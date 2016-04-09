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
    public class MethodScannerTests
    {
        [Theory(DisplayName = "Проверка сканирования всех параметров метода"), AutoFixture]
        public void ScanAllParametersTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IParameterScanner> parameterScannerMock,
            MethodScanner sut)
        {
            var method = moduleDefinition.GetType(Constants.SithNamespace, "DarthMaul")
                .Methods.Single(md => md.Name == "KillJedi");

            sut.Scan(method).ToList();

            parameterScannerMock.Verify(ps => ps.Scan(It.IsAny<ParameterDefinition>()), Times.Exactly(2));
        }

        [Theory(DisplayName = "Проверка сканирования возвращаемого значения метода"), AutoFixture]
        public void ScanAllMethodReturnTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMethodReturnScanner> methodReturnScannerMock,
            MethodScanner sut)
        {
            var method = moduleDefinition.GetType(Constants.SithNamespace, "DarthMaul")
                .Methods.Single(md => md.Name == "KillJedi");

            sut.Scan(method).ToList();

            methodReturnScannerMock.Verify(mrs => mrs.Scan(It.IsAny<MethodReturnType>()), Times.Once);
        }

        [Theory(DisplayName = "Проверка получения атрибута контракта для метода"), AutoFixture]
        public void ScanContractMethodTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            [Frozen]Mock<IParameterScanner> parameterScannerMock,
            [Frozen]Mock<IMethodReturnScanner> methodReturnScannerMock,
            MethodScanner sut)
        {
            var method = moduleDefinition.GetType(Constants.SithNamespace, "DarthMaul")
                .Methods.Single(md => md.Name == "JoinDarkSide");

            parameterScannerMock.Setup(ps => ps.Scan(It.IsAny<ParameterDefinition>())).Returns(Enumerable.Empty<ContractDefinition>);
            methodReturnScannerMock.Setup(mrs => mrs.Scan(It.IsAny<MethodReturnType>())).Returns(Enumerable.Empty<ContractDefinition>);
            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(true);

            var contracts = sut.Scan(method).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Single(contracts);
            Assert.IsAssignableFrom<RequiresDefinition>(contracts.Single());
        }

        [Theory(DisplayName = "Проверка отсутствия атрибута контракта для метода"), AutoFixture]
        public void ScanNoContractMethodTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractCriteria> contractCriteriaMock,
            [Frozen]Mock<IParameterScanner> parameterScannerMock,
            [Frozen]Mock<IMethodReturnScanner> methodReturnScannerMock,
            MethodScanner sut)
        {
            var method = moduleDefinition.GetType(Constants.SithNamespace, "DarthMaul")
                .Methods.Single(md => md.Name == "KillJedi");

            parameterScannerMock.Setup(ps => ps.Scan(It.IsAny<ParameterDefinition>())).Returns(Enumerable.Empty<ContractDefinition>);
            methodReturnScannerMock.Setup(mrs => mrs.Scan(It.IsAny<MethodReturnType>())).Returns(Enumerable.Empty<ContractDefinition>);
            contractCriteriaMock.Setup(cc => cc.IsContract(It.IsAny<CustomAttribute>())).Returns(false);

            var contracts = sut.Scan(method).ToList();

            contractCriteriaMock.Verify(cc => cc.IsContract(It.IsAny<CustomAttribute>()));
            Assert.Empty(contracts);
        }
    }
}
