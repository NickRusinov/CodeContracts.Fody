using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractScanners;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractScanners
{
    public class ContractCriteriaTests
    {
        [Theory(DisplayName = "Проверка определения атрибута контракта")]
        [InlineAutoFixture("Sith", "JoinDarkSide", true)]
        [InlineAutoFixture("Sith", "UseForceLightining", true)]
        [InlineAutoFixture("DarthMaul", "KillJedi", false)]
        public void IsContractAppliedToMethodTest(string typeName, string methodName, bool isContractExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            ContractCriteria sut)
        {
            var customAttribute = moduleDefinition.FindMethod(typeName, methodName).CustomAttributes.First();

            var isContract = sut.IsContract(customAttribute);

            Assert.Equal(isContractExpected, isContract);
        }
    }
}
