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
    public class BestOverloadCriteriaTests
    {
        [Theory(DisplayName = "Проверка применимости метода валидации для определения лучшей перегрузки метода")]
        [InlineAutoFixture("ValidateMinMaxInt", true)]
        [InlineAutoFixture("ValidateMinMaxLong", true)]
        [InlineAutoFixture("ValidateMinMaxComparable", true)]
        [InlineAutoFixture("ValidateMinInt", false)]
        [InlineAutoFixture("ValidateMaxLong", false)]
        public void IsApplyMinIntMaxIntTest(string methodName, bool isApplyExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            BestOverloadCriteria sut)
        {
            var parameterDefinitions = new[]
            {
                new ParameterDefinition("self", 0, moduleDefinition.TypeSystem.Object),
                new ParameterDefinition("arg", 0, moduleDefinition.TypeSystem.Int32),
                new ParameterDefinition("min", 0, moduleDefinition.TypeSystem.Int32),
                new ParameterDefinition("max", 0, moduleDefinition.TypeSystem.Int32),
            };

            var isApply = sut.IsApply(moduleDefinition.FindMethod("MyAttribute", methodName), parameterDefinitions);

            Assert.Equal(isApplyExpected, isApply);
        }

        [Theory(DisplayName = "Проверка применимости метода валидации для определения лучшей перегрузки метода")]
        [InlineAutoFixture("ValidateMinComparable", true)]
        [InlineAutoFixture("ValidateMinInt", false)]
        [InlineAutoFixture("ValidateMinMaxComparable", false)]
        [InlineAutoFixture("ValidateMinMaxLong", false)]
        [InlineAutoFixture("ValidateMaxLong", false)]
        public void IsApplyMinDateTimeTest(string methodName, bool isApplyExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            BestOverloadCriteria sut)
        {
            var parameterDefinitions = new[]
            {
                new ParameterDefinition("self", 0, moduleDefinition.TypeSystem.Object),
                new ParameterDefinition("arg", 0, moduleDefinition.TypeSystem.Int32),
                new ParameterDefinition("min", 0, moduleDefinition.ImportReference(typeof(DateTime))),
            };

            var isApply = sut.IsApply(moduleDefinition.FindMethod("MyAttribute", methodName), parameterDefinitions);

            Assert.Equal(isApplyExpected, isApply);
        }
    }
}
