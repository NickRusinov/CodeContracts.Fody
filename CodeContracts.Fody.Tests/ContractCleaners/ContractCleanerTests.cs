using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractCleaners;
using CodeContracts.Fody.ContractDefinitions;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractCleaners
{
    public class ContractCleanerTests
    {
        [Theory(DisplayName = "Проверка удаления атрибута контракта"), AutoFixture]
        public void AttributeHasBeenCleanTest(
            ContractDefinition contractDefinition,
            ContractCleaner sut)
        {
            sut.Clean(contractDefinition);

            Assert.DoesNotContain(contractDefinition.ContractAttribute, contractDefinition.AttributeProvider.CustomAttributes);
        }
    }
}
