using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectResolvers;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectResolvers
{
    public class InvariantResolverTests
    {
        [Theory(DisplayName = "Проверка вызова получателя контрактного класса для получателя инварианта"), AutoFixture]
        public void ContractClassResolverCalledTest(
            [Frozen]InvariantDefinition invariantDefinition,
            [Frozen]Mock<IContractClassResolver> contractClassResolverMock,
            InvariantResolver sut)
        {
            sut.Resolve(invariantDefinition);

            contractClassResolverMock.Verify(ccr => ccr.Resolve(invariantDefinition.DeclaringType, null));
        }

        [Theory(DisplayName = "Проверка вызова получателя метода инварианта для получателя инварианта"), AutoFixture]
        public void InvariantMethodResolverCalledTest(
            [Frozen]InvariantDefinition invariantDefinition,
            [Frozen]Mock<IInvariantMethodResolver> invariantMethodResolverMock,
            InvariantResolver sut)
        {
            sut.Resolve(invariantDefinition);

            invariantMethodResolverMock.Verify(imr => imr.Resolve(It.IsAny<TypeDefinition>()));
        }
    }
}
