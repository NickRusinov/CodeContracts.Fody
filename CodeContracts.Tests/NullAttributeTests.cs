using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class NullAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта Null")]
        [MemberData(nameof(ValidateArgTestData))]
        public void ValidateArgTest(object arg, bool isValidExpected)
        {
            var isValid = NullAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgTestData()
        {
            yield return new object[] { new object(), false };

            yield return new object[] { "", false };

            yield return new object[] { 1, false };

            yield return new object[] { null, true };
        }
    }
}
