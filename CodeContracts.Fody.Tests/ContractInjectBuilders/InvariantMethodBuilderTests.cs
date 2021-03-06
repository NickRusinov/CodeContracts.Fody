﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectBuilders;
using CodeContracts.Fody.Internal;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectBuilders
{
    public class InvariantMethodBuilderTests
    {
        [Theory(DisplayName = "Проверка создания метода инварианта"), AutoFixture]
        public void MethodHasBeenBuildedTest(
            [Frozen]ModuleDefinition moduleDefinition,
            InvariantMethodBuilder sut)
        {
            var typeDefinition = moduleDefinition.FindType("ConcreteClass");
            var methodsCount = typeDefinition.Methods.Count;

            var invariantMethod = sut.Build(typeDefinition);

            Assert.Contains(invariantMethod, typeDefinition.Methods);
            Assert.Equal(methodsCount + 1, typeDefinition.Methods.Count);
            Assert.True(invariantMethod.IsPrivate);
        }

        [Theory(DisplayName = "Проверка создания метода инварианта с атрибутом ContractInvariantMethod"), AutoFixture]
        public void MethodHasContractInvariantMethodAttribute(
            [Frozen]ModuleDefinition moduleDefinition,
            InvariantMethodBuilder sut)
        {
            var invariantAttribute = moduleDefinition.ImportReference(typeof(ContractInvariantMethodAttribute));
            var typeDefinition = moduleDefinition.FindType("ConcreteClass");

            var invariantMethod = sut.Build(typeDefinition);

            Assert.Single(invariantMethod.CustomAttributes);
            Assert.Single(invariantMethod.CustomAttributes, ca => TypeReferenceComparer.Instance.Equals(ca.AttributeType, invariantAttribute));
        }
    }
}
