using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class RangeAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта Range")]
        [MemberData(nameof(ValidateArgMinMaxOfComparableTestData))]
        public void ValidateArgMinMaxOfComparableTest(IComparable arg, IComparable min, IComparable max, bool isValidExpected)
        {
            var isValid = RangeAttribute.Validate(arg, min, max);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgMinMaxOfComparableTestData()
        {
            yield return new object[] { null, null, null, false };

            yield return new object[] { null, DateTime.Now, DateTime.Now, false };

            yield return new object[] { 555, null, null, false };

            yield return new object[] { 100, 200, 50, false };

            yield return new object[] { new Version(2, 0), new Version(2, 2), new Version(2, 3), false };

            yield return new object[] { new Version(0, 1), new Version(0, 1), new Version(1, 0), true };

            yield return new object[] { new TimeSpan(40), new TimeSpan(30), new TimeSpan(40), true };

            yield return new object[] { new DateTime(256), new DateTime(128), new DateTime(512), true };
        }

        [Theory(DisplayName = "Проверка атрибута контракта Range")]
        [MemberData(nameof(ValidateArgMinMaxOfIntTestData))]
        public void ValidateArgMinMaxOfIntTest(int arg, int min, int max, bool isValidExpected)
        {
            var isValid = RangeAttribute.Validate(arg, min, max);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgMinMaxOfIntTestData()
        {
            yield return new object[] { 10, 20, 10, false };

            yield return new object[] { 23, 23, 333, true };

            yield return new object[] { 56, 0, 56, true };

            yield return new object[] { 42, 42, 42, true };
        }
    }
}
