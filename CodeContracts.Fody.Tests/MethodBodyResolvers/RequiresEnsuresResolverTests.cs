﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.MethodBodyResolvers;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.MethodBodyResolvers
{
    public class RequiresEnsuresResolverTests
    {
        [Theory(DisplayName = "Проверка вызова получателя контрактного класса для получателя предусловия"), AutoFixture]
        public void ContractClassResolverCalledRequiresTest(
            [Frozen]RequiresDefinition requiresDefinition,
            [Frozen]Mock<IContractClassResolver> contractClassResolverMock,
            RequiresEnsuresResolver sut)
        {
            sut.Resolve(requiresDefinition);

            contractClassResolverMock.Verify(ccr => ccr.Resolve(requiresDefinition.DeclaringType, requiresDefinition.ContractMethod));
        }

        [Theory(DisplayName = "Проверка вызова получателя контрактного класса для получателя постусловия"), AutoFixture]
        public void ContractClassResolverCalledEnsuresTest(
            [Frozen]EnsuresDefinition ensuresDefinition,
            [Frozen]Mock<IContractClassResolver> contractClassResolverMock,
            RequiresEnsuresResolver sut)
        {
            sut.Resolve(ensuresDefinition);

            contractClassResolverMock.Verify(ccr => ccr.Resolve(ensuresDefinition.DeclaringType, ensuresDefinition.ContractMethod));
        }

        [Theory(DisplayName = "Проверка получения метода для внедрения предусловия"), AutoFixture]
        public void MethodDefinitionFindRequiresTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractClassResolver> contractClassResolverMock,
            RequiresEnsuresResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthMaul", "JoinDarkSide");
            var requiresDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.First(), methodDefinition.DeclaringType, methodDefinition);
            contractClassResolverMock.Setup(ccr => ccr.Resolve(methodDefinition.DeclaringType, methodDefinition)).Returns(methodDefinition.DeclaringType);

            var injectMethodDefinition = sut.Resolve(requiresDefinition);

            Assert.Same(methodDefinition, injectMethodDefinition);
        }

        [Theory(DisplayName = "Проверка получения переопределенного метода для внедрения предусловия"), AutoFixture]
        public void MethodDefinitionFindOverrideRequiresTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractClassResolver> contractClassResolverMock,
            RequiresEnsuresResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("ISith", "JoinDarkSide");
            var requiresDefinition = new RequiresDefinition(methodDefinition.CustomAttributes.First(), methodDefinition.DeclaringType, methodDefinition);
            var overrideMethodDefinition = moduleDefinition.FindMethod("DarthPlagueis", "JoinDarkSide");
            contractClassResolverMock.Setup(ccr => ccr.Resolve(methodDefinition.DeclaringType, methodDefinition)).Returns(overrideMethodDefinition.DeclaringType);

            var injectMethodDefinition = sut.Resolve(requiresDefinition);

            Assert.Same(overrideMethodDefinition, injectMethodDefinition);
        }

        [Theory(DisplayName = "Проверка получения метода для внедрения постусловия"), AutoFixture]
        public void MethodDefinitionFindEnsuresTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractClassResolver> contractClassResolverMock,
            RequiresEnsuresResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("DarthMaul", "JoinDarkSide");
            var ensuresDefinition = new EnsuresDefinition(methodDefinition.CustomAttributes.First(), methodDefinition.DeclaringType, methodDefinition);
            contractClassResolverMock.Setup(ccr => ccr.Resolve(methodDefinition.DeclaringType, methodDefinition)).Returns(methodDefinition.DeclaringType);

            var injectMethodDefinition = sut.Resolve(ensuresDefinition);

            Assert.Same(methodDefinition, injectMethodDefinition);
        }

        [Theory(DisplayName = "Проверка получения переопределенного метода для внедрения постусловия"), AutoFixture]
        public void MethodDefinitionFindOverrideEnsuresTest(
            [Frozen]ModuleDefinition moduleDefinition,
            [Frozen]Mock<IContractClassResolver> contractClassResolverMock,
            RequiresEnsuresResolver sut)
        {
            var methodDefinition = moduleDefinition.FindMethod("ISith", "JoinDarkSide");
            var ensuresDefinition = new EnsuresDefinition(methodDefinition.CustomAttributes.First(), methodDefinition.DeclaringType, methodDefinition);
            var overrideMethodDefinition = moduleDefinition.FindMethod("DarthPlagueis", "JoinDarkSide");
            contractClassResolverMock.Setup(ccr => ccr.Resolve(methodDefinition.DeclaringType, methodDefinition)).Returns(overrideMethodDefinition.DeclaringType);

            var injectMethodDefinition = sut.Resolve(ensuresDefinition);

            Assert.Same(overrideMethodDefinition, injectMethodDefinition);
        }
    }
}
