using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectBuilders;
using CodeContracts.Fody.ContractInjectResolvers;
using CodeContracts.Fody.Exceptions;
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
            var contractClassA = sut.Resolve(moduleDefinition.FindType("IInterfaceWithContractClass"), moduleDefinition.FindMethod("IInterfaceWithContractClass", "Method"));
            var contractClassB = sut.Resolve(moduleDefinition.FindType("IInterfaceWithContractClass"), null);

            Assert.Same(moduleDefinition.FindType("IInterfaceWithContractClassContracts"), contractClassA);
            Assert.Same(moduleDefinition.FindType("IInterfaceWithContractClassContracts"), contractClassB);
        }

        [Theory(DisplayName = "Проверка разрешения контрактного класса для абстрактного класса с атрибутом ContractClass"), AutoFixture]
        public void AbstractClassWithContractClassTest(
            [Frozen]ModuleDefinition moduleDefinition,
            ContractClassResolver sut)
        {
            var contractClassA = sut.Resolve(moduleDefinition.FindType("AbstractClassWithContractClass"), moduleDefinition.FindMethod("AbstractClassWithContractClass", "AbstractMethod"));
            var contractClassB = sut.Resolve(moduleDefinition.FindType("AbstractClassWithContractClass"), moduleDefinition.FindMethod("AbstractClassWithContractClass", "ConcreteMethod"));
            var contractClassC = sut.Resolve(moduleDefinition.FindType("AbstractClassWithContractClass"), null);

            Assert.Same(moduleDefinition.FindType("AbstractClassWithContractClassContracts"), contractClassA);
            Assert.Same(moduleDefinition.FindType("AbstractClassWithContractClass"), contractClassB);
            Assert.Same(moduleDefinition.FindType("AbstractClassWithContractClass"), contractClassC);
        }

        [Theory(DisplayName = "Проверка разрешения контрактного класса для интерфейса без атрибута ContractClass"), AutoFixture]
        public void InterfaceWithoutContractClassTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IInterfaceContractClassBuilder> interfaceContractClassBuilderMock,
            ContractClassResolver sut)
        {
            var contractClassA = sut.Resolve(moduleDefinition.FindType("IInterface"), moduleDefinition.FindMethod("IInterface", "Method"));
            var contractClassB = sut.Resolve(moduleDefinition.FindType("IInterface"), null);

            interfaceContractClassBuilderMock.Verify(iccb => iccb.Build(moduleDefinition.FindType("IInterface")), Times.Exactly(2));
            interfaceContractClassBuilderMock.Verify(iccb => iccb.Build(moduleDefinition.FindType("IInterface")), Times.Exactly(2));
        }

        [Theory(DisplayName = "Проверка разрешения контрактного класса для абстрактного класса без атрибута ContractClass"), AutoFixture]
        public void AbstractClassWithoutContractClassTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IAbstractContractClassBuilder> abstractContractClassBuilderMock,
            ContractClassResolver sut)
        {
            var contractClassA = sut.Resolve(moduleDefinition.FindType("AbstractClass"), moduleDefinition.FindMethod("AbstractClass", "AbstractMethod"));
            var contractClassB = sut.Resolve(moduleDefinition.FindType("AbstractClass"), moduleDefinition.FindMethod("AbstractClass", "ConcreteMethod"));
            var contractClassC = sut.Resolve(moduleDefinition.FindType("AbstractClass"), null);

            abstractContractClassBuilderMock.Verify(accb => accb.Build(moduleDefinition.FindType("AbstractClass")), Times.Once);
            Assert.Same(moduleDefinition.FindType("AbstractClass"), contractClassB);
            Assert.Same(moduleDefinition.FindType("AbstractClass"), contractClassC);
        }

        [Theory(DisplayName = "Проверка разрешения контрактного класса для метода итератора"), AutoFixture]
        public void IteratorMethodTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractClassResolver sut)
        {
            var iteratorClass = moduleDefinition.FindType("ConcreteClassWithIterators");
            var iteratorMethod = moduleDefinition.FindMethod("ConcreteClassWithIterators", "IteratorMethod");

            Assert.Throws<IteratorMethodsNotSupportedException>(() => sut.Resolve(iteratorClass, iteratorMethod));
        }

        [Theory(DisplayName = "Проверка разрешения контрактного класса для асинхронного метода"), AutoFixture]
        public void AsyncMethodTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractClassResolver sut)
        {
            var iteratorClass = moduleDefinition.FindType("ConcreteClassWithAsyncs");
            var iteratorMethod = moduleDefinition.FindMethod("ConcreteClassWithAsyncs", "AsyncMethod");

            Assert.Throws<AsyncMethodsNotSupportedException>(() => sut.Resolve(iteratorClass, iteratorMethod));
        }
    }
}
