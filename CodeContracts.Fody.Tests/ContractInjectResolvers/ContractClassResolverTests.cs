using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectBuilders;
using CodeContracts.Fody.ContractInjectResolvers;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectResolvers
{
    public class ContractClassResolverTests
    {
        [Theory(DisplayName = "Проверка разрешения контрактного класса для интерфейса с атрибутом ContractClass"), AutoFixture]
        public void InterfaceWithContractClassTest(
            [Frozen]ModuleDefinition moduleDefinition,
            ContractClassResolver sut)
        {
            var contractClassA = sut.Resolve(moduleDefinition.FindType("IJedi"), moduleDefinition.FindMethod("IJedi", "get_Name"));
            var contractClassB = sut.Resolve(moduleDefinition.FindType("IJedi"), null);

            Assert.Same(moduleDefinition.FindType("IJediContracts"), contractClassA);
            Assert.Same(moduleDefinition.FindType("IJediContracts"), contractClassB);
        }

        [Theory(DisplayName = "Проверка разрешения контрактного класса для абстрактного класса с атрибутом ContractClass"), AutoFixture]
        public void AbstractClassWithContractClassTest(
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
        public void InterfaceWithoutContractClassTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IInterfaceContractClassBuilder> interfaceContractClassBuilderMock,
            ContractClassResolver sut)
        {
            var contractClassA = sut.Resolve(moduleDefinition.FindType("ISith"), moduleDefinition.FindMethod("ISith", "get_Name"));
            var contractClassB = sut.Resolve(moduleDefinition.FindType("ISith"), null);

            interfaceContractClassBuilderMock.Verify(iccb => iccb.Build(moduleDefinition.FindType("ISith")), Times.Exactly(2));
            interfaceContractClassBuilderMock.Verify(iccb => iccb.Build(moduleDefinition.FindType("ISith")), Times.Exactly(2));
        }

        [Theory(DisplayName = "Проверка разрешения контрактного класса для абстрактного класса без атрибута ContractClass"), AutoFixture]
        public void AbstractClassWithoutContractClassTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IAbstractContractClassBuilder> abstractContractClassBuilderMock,
            ContractClassResolver sut)
        {
            var contractClassA = sut.Resolve(moduleDefinition.FindType("Sith"), moduleDefinition.FindMethod("Sith", "UseForceLightining"));
            var contractClassB = sut.Resolve(moduleDefinition.FindType("Sith"), moduleDefinition.FindMethod("Sith", "JoinDarkSide"));
            var contractClassC = sut.Resolve(moduleDefinition.FindType("Sith"), null);

            abstractContractClassBuilderMock.Verify(accb => accb.Build(moduleDefinition.FindType("Sith")), Times.Once);
            Assert.Same(moduleDefinition.FindType("Sith"), contractClassB);
            Assert.Same(moduleDefinition.FindType("Sith"), contractClassC);
        }
    }
}
