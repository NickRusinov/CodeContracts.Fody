using CodeContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace CodeContracts.Tests
{
    public class NotNullAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта NotNull")]
        [MemberData(nameof(ValidateArgTestData))]
        public void ValidateArgTest(object arg, bool isValidExpected)
        {
            var isValid = NotNullAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgTestData()
        {
            yield return new object[] { null, false };

            yield return new object[] { new object(), true };

            yield return new object[] { "", true };

            yield return new object[] { 1, true };
        }
    }
}
