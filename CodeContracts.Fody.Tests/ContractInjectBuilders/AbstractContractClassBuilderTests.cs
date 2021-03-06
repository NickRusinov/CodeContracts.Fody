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
    public class AbstractContractClassBuilderTests
    {
        [Theory(DisplayName = "Проверка создания контрактного класса для абстрактного класса"), AutoFixture]
        public void ContractClassHasBeenBuildedTest(
            [Frozen]ModuleDefinition moduleDefinition,
            AbstractContractClassBuilder sut)
        {
            var typeDefinition = moduleDefinition.FindType("AbstractClass");
            var typesCount = moduleDefinition.Types.Count;

            var contractClass = sut.Build(typeDefinition);

            Assert.Contains(contractClass, moduleDefinition.Types);
            Assert.Equal(typesCount + 1, moduleDefinition.Types.Count);
            Assert.Equal(typeDefinition, contractClass.BaseType);
            Assert.True(contractClass.IsNotPublic);
        }

        [Theory(DisplayName = "Проверка наличия у абстрактного класса, для которого создается контрактный класс, атрибута ContractClass"), AutoFixture]
        public void ClassHasContractClassAttribute(
            [Frozen]ModuleDefinition moduleDefinition,
            InterfaceContractClassBuilder sut)
        {
            var contractClassAttribute = moduleDefinition.ImportReference(typeof(ContractClassAttribute));
            var typeDefinition = moduleDefinition.FindType("AbstractClass");

            var contractClass = sut.Build(typeDefinition);

            var attribute = Assert.Single(typeDefinition.CustomAttributes, ca => TypeReferenceComparer.Instance.Equals(ca.AttributeType, contractClassAttribute));
            Assert.Equal(contractClass, attribute.ConstructorArguments.Select(ca => ca.Value).Single());
        }

        [Theory(DisplayName = "Проверка наличия у контрактного класса, который был создан для абстрактного класса, атрибута ContractClassFor"), AutoFixture]
        public void ContractClassHasContractClassForAtribute(
            [Frozen]ModuleDefinition moduleDefinition,
            InterfaceContractClassBuilder sut)
        {
            var contractClassForAttribute = moduleDefinition.ImportReference(typeof(ContractClassForAttribute));
            var typeDefinition = moduleDefinition.FindType("AbstractClass");

            var contractClass = sut.Build(typeDefinition);

            var attribute = Assert.Single(contractClass.CustomAttributes, ca => TypeReferenceComparer.Instance.Equals(ca.AttributeType, contractClassForAttribute));
            Assert.Equal(typeDefinition, attribute.ConstructorArguments.Select(ca => ca.Value).Single());
        }
    }
}
