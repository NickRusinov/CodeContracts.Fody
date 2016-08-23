using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.BestOverloadResolvers;
using CodeContracts.Fody.Tests.Internal;
using Mono.Cecil;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace CodeContracts.Fody.Tests.BestOverloadResolvers
{
    public class BestOverloadMethodComparerTests
    {
        [Theory(DisplayName = "Проверка компаратора параметров для определения лучшей перегрузки метода")]
        [InlineAutoFixture("ValidateWithInt", "ValidateWithLong", +1)]
        [InlineAutoFixture("ValidateWithComparable", "ValidateWithLong", -1)]
        [InlineAutoFixture("ValidateWithComparable", "ValidateWithEnumerable", 0)]
        public void CompareTest(string xMethodName, string yMethodName, int compareExpected,
            [Frozen] ModuleDefinition moduleDefinition,
            BestOverloadMethodComparer sut)
        {
            var x = moduleDefinition.FindMethod("CustomContractWithMethodsAttribute", xMethodName);
            var y = moduleDefinition.FindMethod("CustomContractWithMethodsAttribute", yMethodName);

            var compareXy = sut.Compare(x, y);
            var compareYx = sut.Compare(y, x);

            Assert.Equal(+ 1 * compareExpected, compareXy);
            Assert.Equal(- 1 * compareExpected, compareYx);
        }
    }
}
