using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractValidateResolvers;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.ContractValidateResolvers
{
    public class ContractValidateCriteriaTests
    {
        [Theory(DisplayName = "Проверка критерия для определения методов валидации кнтрактов")]
        [InlineAutoFixture("RedLightsaberAttribute", "ValidateA", true)]
        [InlineAutoFixture("BlueLightsaberAttribute", "ValidateB", true)]
        [InlineAutoFixture("Lightsaber", ".ctor", false)]
        [InlineAutoFixture("Lightsaber", "get_Color", false)]
        [InlineAutoFixture("Lightsaber", "set_Color", false)]
        public void IsContractValidateMethodTest(string typeName, string methodName, bool isContractValidateExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            ContractValidateCriteria sut)
        {
            var methodDefinition = moduleDefinition.FindMethod(typeName, methodName);

            var isContractValidate = sut.IsContractValidate(methodDefinition);

            Assert.Equal(isContractValidateExpected, isContractValidate);
        }
    }
}
