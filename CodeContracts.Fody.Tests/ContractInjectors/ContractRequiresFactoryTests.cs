﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInjectors
{
    public class ContractRequiresFactoryTests
    {
        [Theory(DisplayName = "Проверка фабрики создания метода контракта предусловия с типом и сообщением"), AutoFixture]
        public void CreateContractMethodWithExceptionAndMessageTest(
            string message,
            [Frozen] ModuleDefinition moduleDefinition,
            ContractRequiresFactory sut)
        {
            var typeDefinition = moduleDefinition.ImportReference(typeof(ArgumentNullException)).Resolve();
            var instructionBuilder = sut.Create(moduleDefinition, typeDefinition, message);

            Assert.IsType<ContractMethodWithMessageBuilder>(instructionBuilder);
            Assert.Equal(message, instructionBuilder.FindPrivateField<string>("message"));
            Assert.Equal(ContractReferences.RequiresWithExceptionAndMessage(moduleDefinition, typeDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта предусловия с типом и без сообщения"), AutoFixture]
        public void CreateContractMethodWithExceptionTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractRequiresFactory sut)
        {
            var typeDefinition = moduleDefinition.ImportReference(typeof(ArgumentNullException)).Resolve();
            var instructionBuilder = sut.Create(moduleDefinition, typeDefinition, null);

            Assert.IsType<ContractMethodBuilder>(instructionBuilder);
            Assert.Equal(ContractReferences.RequiresWithException(moduleDefinition, typeDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта предусловия c сообщением без типа"), AutoFixture]
        public void CreateContractMethodWithMessageTest(
            string message,
            [Frozen] ModuleDefinition moduleDefinition,
            ContractRequiresFactory sut)
        {
            var instructionBuilder = sut.Create(moduleDefinition, null, message);

            Assert.IsType<ContractMethodWithMessageBuilder>(instructionBuilder);
            Assert.Equal(message, instructionBuilder.FindPrivateField<string>("message"));
            Assert.Equal(ContractReferences.RequiresWithMessage(moduleDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта предусловия без сообщения и типа"), AutoFixture]
        public void CreateContractMethodTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractRequiresFactory sut)
        {
            var instructionBuilder = sut.Create(moduleDefinition, null, null);

            Assert.IsType<ContractMethodBuilder>(instructionBuilder);
            Assert.Equal(ContractReferences.Requires(moduleDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }
    }
}