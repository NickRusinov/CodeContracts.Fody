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
    public class BestOverloadMethodComparerTests
    {
        [Theory(DisplayName = "Проверка компаратора параметров для определения лучшей перегрузки метода")]
        [InlineAutoFixture("ValidateMinInt", "ValidateMinLong", +1)]
        [InlineAutoFixture("ValidateMinMaxComparable", "ValidateMinMaxLong", -1)]
        [InlineAutoFixture("ValidateMinInt", "ValidateMaxInt", 0)]
        public void CompareTest(string xMethodName, string yMethodName, int compareExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            BestOverloadMethodComparer sut)
        {
            var x = moduleDefinition.FindMethod("MyAttribute", xMethodName);
            var y = moduleDefinition.FindMethod("MyAttribute", yMethodName);

            var compare = sut.Compare(x, y);

            Assert.Equal(compareExpected, compare);
        }
    }
}
