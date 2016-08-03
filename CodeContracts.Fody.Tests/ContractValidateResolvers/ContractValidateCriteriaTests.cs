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
        [Theory(DisplayName = "Проверка критерия для определения методов валидации контрактов")]
        [InlineAutoFixture("CustomContractAttribute", "ValidateMethodA", true)]
        [InlineAutoFixture("CustomContractAttribute", "ValidateMethodB", true)]
        [InlineAutoFixture("ConcreteClassWithProperty", "get_Property", false)]
        [InlineAutoFixture("ConcreteClassWithProperty", "set_Property", false)]
        [InlineAutoFixture("ConcreteClass", ".ctor", false)]
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
