using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            sut.Scan(moduleDefinition.GetType(Constants.SithNamespace, "DarthMaul").Methods.Single(md => md.Name == "KillJedi")).ToList();

            parameterScannerMock.Verify(ps => ps.Scan(It.IsAny<ParameterDefinition>()), Times.Exactly(2));
        }

        [Theory(DisplayName = "Проверка сканирования возвращаемого значения метода"), AutoFixture]
        public void ScanAllMethodReturnTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IMethodReturnScanner> methodReturnScannerMock,
            MethodScanner sut)
        {
            sut.Scan(moduleDefinition.GetType(Constants.SithNamespace, "DarthMaul").Methods.Single(md => md.Name == "KillJedi")).ToList();

            methodReturnScannerMock.Verify(mrs => mrs.Scan(It.IsAny<MethodReturnType>()), Times.Once);
        }
    }
}
