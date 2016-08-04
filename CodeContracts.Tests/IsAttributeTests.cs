using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class IsAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта Is")]
        [MemberData(nameof(ValidateArgTypeTestData))]
        public void ValidateArgTypeTest(object arg, Type type, bool isValidExpected)
        {
            var isValid = IsAttribute.Validate(arg, type);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgTypeTestData()
        {
            yield return new object[] { null, null, false };
            yield return new object[] { null, typeof(object), false };
            yield return new object[] { new Version(), null, false };
            yield return new object[] { new Version(), typeof(DateTime), false };

            yield return new object[] { 0, typeof(object), true };
            yield return new object[] { 0, typeof(int), true };
            yield return new object[] { new Version(), typeof(IComparable), true };
        }
    }
}
