using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class NotEqualsAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта NotEquals")]
        [MemberData(nameof(ValidateArgToOfObjectTestData))]
        public void ValidateArgToOfObjectTest(object arg, object to, bool isValidExpected)
        {
            var isValid = NotEqualsAttribute.Validate(arg, to);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgToOfObjectTestData()
        {
            yield return new object[] { null, null, false };

            yield return new object[] { new DateTime(256), new DateTime(256), false };
            
            var classInstance = new EventArgs();
            yield return new object[] { classInstance, classInstance, false };

            yield return new object[] { new object(), new object(), true };
            
            yield return new object[] { null, DBNull.Value, true };

            yield return new object[] { new Version(1, 1), null, true };
        }

        [Theory(DisplayName = "Проверка атрибута контракта NotEquals")]
        [InlineData(42, 42, false)]
        [InlineData(10, 20, true)]
        public void ValidateArgToOfIntTest(int arg, int to, bool isValidExpected)
        {
            var isValid = NotEqualsAttribute.Validate(arg, to);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта NotEquals")]
        [InlineData(42u, 42u, false)]
        [InlineData(10u, 20u, true)]
        public void ValidateArgToOfUIntTest(uint arg, uint to, bool isValidExpected)
        {
            var isValid = NotEqualsAttribute.Validate(arg, to);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта NotEquals")]
        [InlineData(42L, 42L, false)]
        [InlineData(10L, 20L, true)]
        public void ValidateArgToOfLongTest(long arg, long to, bool isValidExpected)
        {
            var isValid = NotEqualsAttribute.Validate(arg, to);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта NotEquals")]
        [InlineData(42uL, 42uL, false)]
        [InlineData(10uL, 20uL, true)]
        public void ValidateArgToOfULongTest(ulong arg, ulong to, bool isValidExpected)
        {
            var isValid = NotEqualsAttribute.Validate(arg, to);

            Assert.Equal(isValidExpected, isValid);
        }
    }
}
