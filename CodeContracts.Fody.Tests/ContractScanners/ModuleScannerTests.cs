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
    public class ModuleScannerTests
    {
        [Theory(DisplayName = "Проверка сканирования всех типов в сборке"), AutoFixture]
        public void ScanAllTypesTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<ITypeScanner> typeScannerMock,
            ModuleScanner sut)
        {
            var contracts = sut.Scan(moduleDefinition).ToList();

            typeScannerMock.Verify(ts => ts.Scan(It.IsAny<TypeDefinition>()), Times.Exactly(14));
            Assert.Equal(42, contracts.Count);
        }
    }
}
