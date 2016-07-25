using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class ZeroAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта Zero")]
        [MemberData(nameof(ValidateArgTestData))]
        public void ValidateArgTest(int arg, bool isValidExpected)
        {
            var isValid = ZeroAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgTestData()
        {
            yield return new object[] { 1, false };

            yield return new object[] { 0, true };
        }
    }
}
