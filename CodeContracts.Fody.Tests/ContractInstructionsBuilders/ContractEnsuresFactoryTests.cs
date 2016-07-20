using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInstructionsBuilders;
using CodeContracts.Fody.Internal;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractInstructionsBuilders
{
    public class ContractEnsuresFactoryTests
    {
        [Theory(DisplayName = "Проверка фабрики создания метода контракта постусловия с сообщением")]
        [InlineAutoFixture(typeof(ArgumentNullException))]
        [InlineAutoFixture(null)]
        public void CreateContractMethodWithMessageTest(Type type, 
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] ContractConfig contractConfig,
            string message,
            ContractEnsuresFactory sut)
        {
            contractConfig.Ensures = EnsuresMode.WithMessages;
            var typeDefinition = type != null ? moduleDefinition.ImportReference(type).Resolve() : null;
            var instructionBuilder = sut.Create(typeDefinition, message);

            Assert.IsType<ContractMethodWithMessageBuilder>(instructionBuilder);
            Assert.Equal(message, instructionBuilder.FindPrivateField<string>("message"));
            Assert.Equal(moduleDefinition.ImportEnsuresWithMessage(), instructionBuilder.FindPrivateField<MethodReference>("methodReference"), MethodReferenceComparer.Instance);
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта постусловия с сообщением")]
        [InlineAutoFixture(typeof(ArgumentNullException))]
        [InlineAutoFixture(null)]
        public void CreateContractMethodWithoutMessagesModeTest(Type type,
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] ContractConfig contractConfig,
            string message,
            ContractEnsuresFactory sut)
        {
            contractConfig.Ensures = EnsuresMode.WithoutMessages;
            var typeDefinition = type != null ? moduleDefinition.ImportReference(type).Resolve() : null;
            var instructionBuilder = sut.Create(typeDefinition, message);

            Assert.IsType<ContractMethodBuilder>(instructionBuilder);
            Assert.Equal(moduleDefinition.ImportEnsures(), instructionBuilder.FindPrivateField<MethodReference>("methodReference"), MethodReferenceComparer.Instance);
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта постусловия без сообщения")]
        [InlineAutoFixture(typeof(ArgumentNullException))]
        [InlineAutoFixture(null)]
        public void CreateContractMethodTest(Type type,
            [Frozen] ModuleDefinition moduleDefinition,
            ContractEnsuresFactory sut)
        {
            var typeDefinition = type != null ? moduleDefinition.ImportReference(type).Resolve() : null;
            var instructionBuilder = sut.Create(typeDefinition, null);

            Assert.IsType<ContractMethodBuilder>(instructionBuilder);
            Assert.Equal(moduleDefinition.ImportEnsures(), instructionBuilder.FindPrivateField<MethodReference>("methodReference"), MethodReferenceComparer.Instance);
        }
    }
}
