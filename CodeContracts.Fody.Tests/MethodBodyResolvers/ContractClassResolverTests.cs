using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.MethodBodyResolvers;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.MethodBodyResolvers
{
    public class ContractClassResolverTests
    {
        [Theory(DisplayName = "Проверка разрешения контрактного класса для интерфейса с атрибутом ContractClass"), AutoFixture]
        public void InterfaceWithContractClass(
            [Frozen]ModuleDefinition moduleDefinition,
            ContractClassResolver sut)
        {
            var contractClassA = sut.Resolve(moduleDefinition.FindType("IJedi"), moduleDefinition.FindMethod("IJedi", "get_Name"));
            var contractClassB = sut.Resolve(moduleDefinition.FindType("IJedi"), null);

            Assert.Same(moduleDefinition.FindType("IJediContracts"), contractClassA);
            Assert.Same(moduleDefinition.FindType("IJediContracts"), contractClassB);
        }

        [Theory(DisplayName = "Проверка разрешения контрактного класса для абстрактного класса с атрибутом ContractClass"), AutoFixture]
        public void AbstractClassWithContractClass(
            [Frozen]ModuleDefinition moduleDefinition,
            ContractClassResolver sut)
        {
            var contractClassA = sut.Resolve(moduleDefinition.FindType("Jedi"), moduleDefinition.FindMethod("Jedi", "UseTheForce"));
            var contractClassB = sut.Resolve(moduleDefinition.FindType("Jedi"), moduleDefinition.FindMethod("Jedi", "get_OrderRank"));
            var contractClassC = sut.Resolve(moduleDefinition.FindType("Jedi"), null);

            Assert.Same(moduleDefinition.FindType("JediContracts"), contractClassA);
            Assert.Same(moduleDefinition.FindType("Jedi"), contractClassB);
            Assert.Same(moduleDefinition.FindType("Jedi"), contractClassC);
        }

        [Theory(DisplayName = "Проверка разрешения контрактного класса для интерфейса без атрибута ContractClass"), AutoFixture]
        public void InterfaceWithoutContractClass(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractClassResolver> contractClassResolverMock,
            ContractClassResolver sut)
        {
            var contractClassA = sut.Resolve(moduleDefinition.FindType("ISith"), moduleDefinition.FindMethod("ISith", "get_Name"));
            var contractClassB = sut.Resolve(moduleDefinition.FindType("ISith"), null);

            contractClassResolverMock.Verify(ccr => ccr.Resolve(moduleDefinition.FindType("ISith"), moduleDefinition.FindMethod("ISith", "get_Name")), Times.Once);
            contractClassResolverMock.Verify(ccr => ccr.Resolve(moduleDefinition.FindType("ISith"), null), Times.Once);
        }

        [Theory(DisplayName = "Проверка разрешения контрактного класса для абстрактного класса без атрибута ContractClass"), AutoFixture]
        public void AbstractClassWithoutContractClass(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractClassResolver> contractClassResolverMock,
            ContractClassResolver sut)
        {
            var contractClassA = sut.Resolve(moduleDefinition.FindType("Sith"), moduleDefinition.FindMethod("Sith", "UseForceLightining"));
            var contractClassB = sut.Resolve(moduleDefinition.FindType("Sith"), moduleDefinition.FindMethod("Sith", "JoinDarkSide"));
            var contractClassC = sut.Resolve(moduleDefinition.FindType("Sith"), null);

            contractClassResolverMock.Verify(ccr => ccr.Resolve(moduleDefinition.FindType("Sith"), moduleDefinition.FindMethod("Sith", "UseForceLightining")), Times.Once);
            Assert.Same(moduleDefinition.FindType("Sith"), contractClassB);
            Assert.Same(moduleDefinition.FindType("Sith"), contractClassC);
        }
    }
}
