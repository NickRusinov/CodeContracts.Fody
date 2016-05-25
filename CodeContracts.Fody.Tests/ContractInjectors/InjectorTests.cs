using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.ContractInjectors;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class InjectorTests
    {
        [Theory(DisplayName = "Проверка вызова инжектора для внедрения предусловия"), AutoFixture]
        public void RequiresInjectorHasBeenCalled(
            [Frozen] Mock<IRequiresInjector> requiresInjectorMock,
            RequiresDefinition requiresDefinition,
            MethodDefinition methodDefinition,
            Injector sut)
        {
            sut.Inject(requiresDefinition, methodDefinition);

            requiresInjectorMock.Verify(ri => ri.Inject(requiresDefinition, methodDefinition), Times.Once);
        }

        [Theory(DisplayName = "Проверка вызова инжектора для внедрения постусловия"), AutoFixture]
        public void EnsuresInjectorHasBeenCalled(
            [Frozen] Mock<IEnsuresInjector> ensuresInjectorMock,
            EnsuresDefinition ensuresDefinition,
            MethodDefinition methodDefinition,
            Injector sut)
        {
            sut.Inject(ensuresDefinition, methodDefinition);

            ensuresInjectorMock.Verify(ei => ei.Inject(ensuresDefinition, methodDefinition), Times.Once);
        }
        [Theory(DisplayName = "Проверка вызова инжектора для внедрения инварианта"), AutoFixture]
        public void InvariantInjectorHasBeenCalled(
            [Frozen] Mock<IInvariantInjector> invariantInjectorMock,
            InvariantDefinition invariantDefinition,
            MethodDefinition methodDefinition,
            Injector sut)
        {
            sut.Inject(invariantDefinition, methodDefinition);

            invariantInjectorMock.Verify(ii => ii.Inject(invariantDefinition, methodDefinition), Times.Once);
        }
    }
}
