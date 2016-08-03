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
    public class InvariantMethodResolverTests
    {
        [Theory(DisplayName = "Проверка разрешения метода инварианта для класса, содержащего метод инвариант"), AutoFixture]
        public void ClassWithInvariantMethodTest(
            [Frozen]ModuleDefinition moduleDefinition,
            InvariantMethodResolver sut)
        {
            var invariantMethod = sut.Resolve(moduleDefinition.FindType("ConcreteClassWithInvariant"));

            Assert.Same(moduleDefinition.FindMethod("ConcreteClassWithInvariant", "InvariantMethod"), invariantMethod);
        }

        [Theory(DisplayName = "Проверка разрешения метода инварианта для класса, не содержащего метод инвариант"), AutoFixture]
        public void ClassWithoutInvariantMethodTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IInvariantMethodBuilder> invariantMethodBuilderMock,
            InvariantMethodResolver sut)
        {
            var invariantMethod = sut.Resolve(moduleDefinition.FindType("ConcreteClass"));

            invariantMethodBuilderMock.Verify(imb => imb.Build(moduleDefinition.FindType("ConcreteClass")), Times.Once);
        }
    }
}
