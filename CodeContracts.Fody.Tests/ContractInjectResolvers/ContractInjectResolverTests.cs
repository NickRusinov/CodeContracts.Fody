using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectResolvers;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectResolvers
{
    public class ContractInjectResolverTests
    {
        [Theory(DisplayName = "Проверка вызова разрешения метода для инъекции в него предусловия"), AutoFixture]
        public void RequiresResolverHasBeenCalled(
            [Frozen] Mock<IRequiresResolver> requiresResolverMock,
            RequiresDefinition requiresDefinition,
            ContractInjectResolver sut)
        {
            sut.Resolve(requiresDefinition);

            requiresResolverMock.Verify(rr => rr.Resolve(requiresDefinition), Times.Once);
        }

        [Theory(DisplayName = "Проверка вызова разрешения метода для инъекции в него постусловия"), AutoFixture]
        public void EnsuresResolverHasBeenCalled(
            [Frozen] Mock<IEnsuresResolver> ensuresResolverMock,
            EnsuresDefinition ensuresDefinition,
            ContractInjectResolver sut)
        {
            sut.Resolve(ensuresDefinition);

            ensuresResolverMock.Verify(er => er.Resolve(ensuresDefinition), Times.Once);
        }
        [Theory(DisplayName = "Проверка вызова разрешения метода для инъекции в него инварианта"), AutoFixture]
        public void InvariantResolverHasBeenCalled(
            [Frozen] Mock<IInvariantResolver> invariantResolverMock,
            InvariantDefinition invariantDefinition,
            ContractInjectResolver sut)
        {
            sut.Resolve(invariantDefinition);

            invariantResolverMock.Verify(ir => ir.Resolve(invariantDefinition), Times.Once);
        }
    }
}
