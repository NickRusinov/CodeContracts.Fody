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
    public class DisabledContractCleanerTests
    {
        [Theory(DisplayName = "Проверка неудаления атрибута контракта"), AutoFixture]
        public void AttributeHasNotBeenCleanTest(
            ContractDefinition contractDefinition,
            DisabledContractCleaner sut)
        {
            sut.Clean(contractDefinition);

            Assert.Contains(contractDefinition.ContractAttribute, contractDefinition.AttributeProvider.CustomAttributes);
        }
    }
}
