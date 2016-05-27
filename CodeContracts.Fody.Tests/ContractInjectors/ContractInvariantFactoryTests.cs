using System;
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
    public class ContractInvariantFactoryTests
    {
        [Theory(DisplayName = "Проверка фабрики создания метода контракта инварианта с сообщением")]
        [InlineAutoFixture(typeof(ArgumentNullException))]
        [InlineAutoFixture(null)]
        public void CreateContractMethodWithMessageTest(Type type,
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] ContractConfig contractConfig,
            string message,
            ContractInvariantFactory sut)
        {
            contractConfig.Invariant = InvariantMode.WithMessages;
            var typeDefinition = type != null ? moduleDefinition.ImportReference(type).Resolve() : null;
            var instructionBuilder = sut.Create(typeDefinition, message);

            Assert.IsType<ContractMethodWithMessageBuilder>(instructionBuilder);
            Assert.Equal(message, instructionBuilder.FindPrivateField<string>("message"));
            Assert.Equal(ContractReferences.InvariantWithMessage(moduleDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта инварианта с сообщением")]
        [InlineAutoFixture(typeof(ArgumentNullException))]
        [InlineAutoFixture(null)]
        public void CreateContractMethodWithoutMessagesModeTest(Type type,
            [Frozen] ModuleDefinition moduleDefinition,
            [Frozen] ContractConfig contractConfig,
            string message,
            ContractInvariantFactory sut)
        {
            contractConfig.Invariant = InvariantMode.WithoutMessages;
            var typeDefinition = type != null ? moduleDefinition.ImportReference(type).Resolve() : null;
            var instructionBuilder = sut.Create(typeDefinition, message);

            Assert.IsType<ContractMethodBuilder>(instructionBuilder);
            Assert.Equal(ContractReferences.Invariant(moduleDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }

        [Theory(DisplayName = "Проверка фабрики создания метода контракта инварианта без сообщения")]
        [InlineAutoFixture(typeof(ArgumentNullException))]
        [InlineAutoFixture(null)]
        public void CreateContractMethodTest(Type type,
            [Frozen] ModuleDefinition moduleDefinition,
            ContractInvariantFactory sut)
        {
            var typeDefinition = type != null ? moduleDefinition.ImportReference(type).Resolve() : null;
            var instructionBuilder = sut.Create(typeDefinition, null);

            Assert.IsType<ContractMethodBuilder>(instructionBuilder);
            Assert.Equal(ContractReferences.Invariant(moduleDefinition).Resolve(), instructionBuilder.FindPrivateField<MethodReference>("methodReference").Resolve());
        }
    }
}
