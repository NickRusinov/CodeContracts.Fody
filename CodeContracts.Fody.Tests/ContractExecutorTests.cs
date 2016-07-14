using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractCleaners;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractInjectResolvers;
using CodeContracts.Fody.ContractScanners;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests
{
    public class ContractExecutorTests
    {
        [Theory(DisplayName = "Проверка вызова сканера сборки при выполнении основного алгоритма"), AutoFixture]
        public void ModuleScannerHasBeenCalledTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] Mock<IModuleScanner> moduleScannerMock,
            ContractExecutor sut)
        {
            sut.Execute(moduleDefinition);

            moduleScannerMock.Verify(ms => ms.Scan(moduleDefinition), Times.Once);
        }

        [Theory(DisplayName = "Проверка вызова получателя метода для внедрения контрактов при выполнении основного алгоритма"), AutoFixture]
        public void ContractInjectResolverHasBeenCalledTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] Mock<IContractInjectResolver> contractInjectResolverMock,
            ContractExecutor sut)
        {
            sut.Execute(moduleDefinition);

            contractInjectResolverMock.Verify(cir => cir.Resolve(It.IsAny<ContractDefinition>()), Times.Exactly(3));
        }

        [Theory(DisplayName = "Проверка вызова инжектора контрактов при выполнении основного алгоритма"), AutoFixture]
        public void ContractInjectorHasBeenCalledTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] Mock<IContractInjector> contractInjectorMock,
            ContractExecutor sut)
        {
            sut.Execute(moduleDefinition);

            contractInjectorMock.Verify(ci => ci.Inject(It.IsAny<ContractDefinition>(), It.IsAny<MethodDefinition>()), Times.Exactly(3));
        }

        [Theory(DisplayName = "Проверка вызова очистителя контрактов при выполнении основного алгоритма"), AutoFixture]
        public void ContractClenerHasBeenCalledTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] Mock<IContractCleaner> contractCleanerMock,
            ContractExecutor sut)
        {
            sut.Execute(moduleDefinition);

            contractCleanerMock.Verify(cc => cc.Clean(It.IsAny<ContractDefinition>()), Times.Exactly(3));
        }
    }
}
