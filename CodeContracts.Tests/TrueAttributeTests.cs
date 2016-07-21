using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class TrueAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта True")]
        [MemberData(nameof(ValidateArgTestData))]
        public void ValidateArgTest(bool arg, bool isValidExpected)
        {
            var isValid = TrueAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgTestData()
        {
            yield return new object[] { false, false };

            yield return new object[] { true, true };
        }
    }
}
