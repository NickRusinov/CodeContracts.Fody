using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class EqualsAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта Equals")]
        [MemberData(nameof(ValidateArgToOfObjectTestData))]
        public void ValidateArgToOfObjectTest(object arg, object to, bool isValidExpected)
        {
            var isValid = EqualsAttribute.Validate(arg, to);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgToOfObjectTestData()
        {
            yield return new object[] { new object(), new object(), false };
            
            yield return new object[] { null, DBNull.Value, false };

            yield return new object[] { new Version(1, 1), null, false };
            
            yield return new object[] { null, null, true };

            yield return new object[] { new DateTime(256), new DateTime(256), true };
            
            var classInstance = new EventArgs();
            yield return new object[] { classInstance, classInstance, true };
        }

        [Theory(DisplayName = "Проверка атрибута контракта Equals")]
        [InlineData(10, 20, false)]
        [InlineData(42, 42, true)]
        public void ValidateArgToOfIntTest(int arg, int to, bool isValidExpected)
        {
            var isValid = EqualsAttribute.Validate(arg, to);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Equals")]
        [InlineData(10u, 20u, false)]
        [InlineData(42u, 42u, true)]
        public void ValidateArgToOfUIntTest(uint arg, uint to, bool isValidExpected)
        {
            var isValid = EqualsAttribute.Validate(arg, to);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Equals")]
        [InlineData(10L, 20L, false)]
        [InlineData(42L, 42L, true)]
        public void ValidateArgToOfLongTest(long arg, long to, bool isValidExpected)
        {
            var isValid = EqualsAttribute.Validate(arg, to);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Equals")]
        [InlineData(10uL, 20uL, false)]
        [InlineData(42uL, 42uL, true)]
        public void ValidateArgToOfULongTest(ulong arg, ulong to, bool isValidExpected)
        {
            var isValid = EqualsAttribute.Validate(arg, to);

            Assert.Equal(isValidExpected, isValid);
        }
    }
}
