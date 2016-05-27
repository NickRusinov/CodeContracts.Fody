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
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] ContractConfig contractConfig,
            string message,
            ContractRequiresFactory sut)
        {
            contractConfig.Requires = RequiresMode.WithMessages | RequiresMode.WithExceptions;
            var typeDefinition = moduleDefinition.ImportReference(typeof(ArgumentNullException)).Resolve();
            var instructionBuilder = sut.Create(typeDefinition, message);

            Assert.IsType<ContractMethodWithMessageBuilder>(instructionBuilder);
            Assert.Equal(message, instructionBuilder.FindPrivateField<string>("message"));
            Assert.Equal(ContractReferences.RequiresWithExceptionAndMessage(moduleDefinition, typeDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта предусловия с типом и сообщением"), AutoFixture]
        public void CreateContractMethodWithoutExceptionsAndWithoutMessagesModeTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] ContractConfig contractConfig,
            string message,
            ContractRequiresFactory sut)
        {
            contractConfig.Requires = RequiresMode.WithoutMessages | RequiresMode.WithoutExceptions;
            var typeDefinition = moduleDefinition.ImportReference(typeof(ArgumentNullException)).Resolve();
            var instructionBuilder = sut.Create(typeDefinition, message);

            Assert.IsType<ContractMethodBuilder>(instructionBuilder);
            Assert.Equal(ContractReferences.Requires(moduleDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта предусловия с типом и без сообщения"), AutoFixture]
        public void CreateContractMethodWithExceptionTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] ContractConfig contractConfig,
            ContractRequiresFactory sut)
        {
            contractConfig.Requires = RequiresMode.WithExceptions;
            var typeDefinition = moduleDefinition.ImportReference(typeof(ArgumentNullException)).Resolve();
            var instructionBuilder = sut.Create(typeDefinition, null);

            Assert.IsType<ContractMethodBuilder>(instructionBuilder);
            Assert.Equal(ContractReferences.RequiresWithException(moduleDefinition, typeDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта предусловия с типом и без сообщения"), AutoFixture]
        public void CreateContractMethodWithoutExceptionsModeTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] ContractConfig contractConfig,
            ContractRequiresFactory sut)
        {
            contractConfig.Requires = RequiresMode.WithoutExceptions;
            var typeDefinition = moduleDefinition.ImportReference(typeof(ArgumentNullException)).Resolve();
            var instructionBuilder = sut.Create(typeDefinition, null);

            Assert.IsType<ContractMethodBuilder>(instructionBuilder);
            Assert.Equal(ContractReferences.Requires(moduleDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта предусловия c сообщением без типа"), AutoFixture]
        public void CreateContractMethodWithMessageTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] ContractConfig contractConfig,
            string message,
            ContractRequiresFactory sut)
        {
            contractConfig.Requires = RequiresMode.WithMessages;
            var instructionBuilder = sut.Create(null, message);

            Assert.IsType<ContractMethodWithMessageBuilder>(instructionBuilder);
            Assert.Equal(message, instructionBuilder.FindPrivateField<string>("message"));
            Assert.Equal(ContractReferences.RequiresWithMessage(moduleDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта предусловия c сообщением без типа"), AutoFixture]
        public void CreateContractMethodWithoutMessagesModeTest(
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] ContractConfig contractConfig,
            string message,
            ContractRequiresFactory sut)
        {
            contractConfig.Requires = RequiresMode.WithoutMessages;
            var instructionBuilder = sut.Create(null, message);

            Assert.IsType<ContractMethodBuilder>(instructionBuilder);
            Assert.Equal(ContractReferences.Requires(moduleDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта предусловия без сообщения и типа"), AutoFixture]
        public void CreateContractMethodTest(
            [Frozen] ModuleDefinition moduleDefinition,
            ContractRequiresFactory sut)
        {
            var instructionBuilder = sut.Create(null, null);

            Assert.IsType<ContractMethodBuilder>(instructionBuilder);
            Assert.Equal(ContractReferences.Requires(moduleDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }
    }
}
