﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.MethodBodyResolvers;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.MethodBodyResolvers
{
    public class InterfaceContractClassBuilderTests
    {
        [Theory(DisplayName = "Проверка создания контрактного класса для интерфейса"), AutoFixture]
        public void ContractClassHasBeenBuildedTest(
            [Frozen]ModuleDefinition moduleDefinition,
            InterfaceContractClassBuilder sut)
        {
            var typeDefinition = moduleDefinition.FindType("ISith");
            var typesCount = moduleDefinition.Types.Count;

            var contractClass = sut.Build(typeDefinition);

            Assert.Contains(contractClass, moduleDefinition.Types);
            Assert.Equal(typesCount + 1, moduleDefinition.Types.Count);
            Assert.Contains(typeDefinition, contractClass.Interfaces);
            Assert.True(contractClass.IsNotPublic);
        }

        [Theory(DisplayName = "Проверка наличия у интерфейса, для которого создается контрактный класс, атрибута ContractClass"), AutoFixture]
        public void ClassHasContractClassAttribute(
            [Frozen]ModuleDefinition moduleDefinition,
            InterfaceContractClassBuilder sut)
        {
            var contractClassAttribute = moduleDefinition.ImportReference(typeof(ContractClassAttribute));
            var typeDefinition = moduleDefinition.FindType("ISith");

            var contractClass = sut.Build(typeDefinition);

            var attribute = Assert.Single(typeDefinition.CustomAttributes, ca => Equals(ca.AttributeType.Resolve(), contractClassAttribute.Resolve()));
            Assert.Equal(contractClass, attribute.ConstructorArguments.Select(ca => ca.Value).Single());
        }

        [Theory(DisplayName = "Проверка наличия у контрактного класса, который был создан для интерфейса, атрибута ContractClassFor"), AutoFixture]
        public void ContractClassHasContractClassForAtribute(
            [Frozen]ModuleDefinition moduleDefinition,
            InterfaceContractClassBuilder sut)
        {
            var contractClassForAttribute = moduleDefinition.ImportReference(typeof(ContractClassForAttribute));
            var typeDefinition = moduleDefinition.FindType("ISith");

            var contractClass = sut.Build(typeDefinition);

            var attribute = Assert.Single(contractClass.CustomAttributes, ca => Equals(ca.AttributeType.Resolve(), contractClassForAttribute.Resolve()));
            Assert.Equal(typeDefinition, attribute.ConstructorArguments.Select(ca => ca.Value).Single());
        }
    }
}