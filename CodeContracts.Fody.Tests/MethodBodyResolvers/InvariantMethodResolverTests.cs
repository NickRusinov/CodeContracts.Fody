using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.MethodBodyResolvers;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.MethodBodyResolvers
{
    public class InvariantMethodResolverTests
    {
        [Theory(DisplayName = "Проверка разрешения метода инварианта для класса, содержащего метод инвариант"), AutoFixture]
        public void ClassWithInvariantMethodTest(
            [Frozen]ModuleDefinition moduleDefinition,
            InvariantMethodResolver sut)
        {
            var invariantMethod = sut.Resolve(moduleDefinition.FindType("Jedi"));

            Assert.Same(moduleDefinition.FindMethod("Jedi", "Invariant"), invariantMethod);
        }

        [Theory(DisplayName = "Проверка разрешения метода инварианта для класса, не содержащего метод инвариант"), AutoFixture]
        public void ClassWithoutInvariantMethodTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IInvariantMethodResolver> invariantMethodResolverMock,
            InvariantMethodResolver sut)
        {
            var invariantMethod = sut.Resolve(moduleDefinition.FindType("Sith"));

            invariantMethodResolverMock.Verify(imr => imr.Resolve(moduleDefinition.FindType("Sith")), Times.Once);
        }
    }
}
