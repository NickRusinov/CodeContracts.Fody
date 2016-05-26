using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Fody.Tests
{
    public class ContractConfigParserTests
    {
        [Theory(DisplayName = "Проверка парсера конфигурации")]
        [InlineAutoFixture(@"<CodeContracts />", true)]
        [InlineAutoFixture(@"<CodeContracts IsEnabled=""true"" />", true)]
        [InlineAutoFixture(@"<CodeContracts IsEnabled=""false"" />", false)]
        public void IsEnabledParseTest(string configString, bool isEnabledExpected,
            ContractConfigParser sut)
        {
            var contractConfig = sut.Parse(configString);

            Assert.Equal(isEnabledExpected, contractConfig.IsEnabled);
        }

        [Theory(DisplayName = "Проверка парсера конфигурации для предусловий")]
        [InlineAutoFixture(@"<CodeContracts />", RequiresMode.WithoutMessages | RequiresMode.WithoutExceptions)]
        [InlineAutoFixture(@"<CodeContracts Requires=""WithMessages"" />", RequiresMode.WithMessages | RequiresMode.WithoutExceptions)]
        [InlineAutoFixture(@"<CodeContracts Requires=""WithoutExceptions"" />", RequiresMode.WithoutMessages | RequiresMode.WithoutExceptions)]
        [InlineAutoFixture(@"<CodeContracts Requires=""WithoutMessages WithExceptions"" />", RequiresMode.WithoutMessages | RequiresMode.WithExceptions)]
        [InlineAutoFixture(@"<CodeContracts Requires=""WithMessages WithoutExceptions"" />", RequiresMode.WithMessages | RequiresMode.WithoutExceptions)]
        public void RequiresParseTest(string configString, RequiresMode requiresModeExpected,
            ContractConfigParser sut)
        {
            var contractConfig = sut.Parse(configString);

            Assert.Equal(requiresModeExpected, contractConfig.Requires);
        }

        [Theory(DisplayName = "Проверка парсера конфигурации для постусловий")]
        [InlineAutoFixture(@"<CodeContracts />", EnsuresMode.WithoutMessages)]
        [InlineAutoFixture(@"<CodeContracts Ensures=""WithMessages"" />", EnsuresMode.WithMessages)]
        [InlineAutoFixture(@"<CodeContracts Ensures=""WithoutMessages"" />", EnsuresMode.WithoutMessages)]
        public void EnsuresParseTest(string configString, EnsuresMode ensuresModeExpected,
            ContractConfigParser sut)
        {
            var contractConfig = sut.Parse(configString);

            Assert.Equal(ensuresModeExpected, contractConfig.Ensures);
        }

        [Theory(DisplayName = "Проверка парсера конфигурации для инвариантов")]
        [InlineAutoFixture(@"<CodeContracts />", InvariantMode.WithoutMessages)]
        [InlineAutoFixture(@"<CodeContracts Invariant=""WithMessages"" />", InvariantMode.WithMessages)]
        [InlineAutoFixture(@"<CodeContracts Invariant=""WithoutMessages"" />", InvariantMode.WithoutMessages)]
        public void InvariantParseTest(string configString, InvariantMode invariantModeExpected,
            ContractConfigParser sut)
        {
            var contractConfig = sut.Parse(configString);

            Assert.Equal(invariantModeExpected, contractConfig.Invariant);
        }
    }
}
