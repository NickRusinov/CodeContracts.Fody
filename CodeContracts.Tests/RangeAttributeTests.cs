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
        [InlineData(10, 20, 10, false)]
        [InlineData(23, 23, 333, true)]
        [InlineData(56, 0, 56, true)]
        [InlineData(42, 42, 42, true)]
        public void ValidateArgMinMaxOfIntTest(int arg, int min, int max, bool isValidExpected)
        {
            var isValid = RangeAttribute.Validate(arg, min, max);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Range")]
        [InlineData(10u, 20u, 10u, false)]
        [InlineData(23u, 23u, 333u, true)]
        [InlineData(56u, 0u, 56u, true)]
        [InlineData(42u, 42u, 42u, true)]
        public void ValidateArgMinMaxOfUIntTest(uint arg, uint min, uint max, bool isValidExpected)
        {
            var isValid = RangeAttribute.Validate(arg, min, max);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Range")]
        [InlineData(10L, 20L, 10L, false)]
        [InlineData(23L, 23L, 333L, true)]
        [InlineData(56L, 0L, 56L, true)]
        [InlineData(42L, 42L, 42L, true)]
        public void ValidateArgMinMaxOfLongTest(long arg, long min, long max, bool isValidExpected)
        {
            var isValid = RangeAttribute.Validate(arg, min, max);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Range")]
        [InlineData(10uL, 20uL, 10uL, false)]
        [InlineData(23uL, 23uL, 333uL, true)]
        [InlineData(56uL, 0uL, 56uL, true)]
        [InlineData(42uL, 42uL, 42uL, true)]
        public void ValidateArgMinMaxOfULongTest(ulong arg, ulong min, ulong max, bool isValidExpected)
        {
            var isValid = RangeAttribute.Validate(arg, min, max);

            Assert.Equal(isValidExpected, isValid);
        }
    }
}
