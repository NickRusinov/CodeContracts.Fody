using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class NotZeroAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта NotZero")]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public void ValidateArgTest(int arg, bool isValidExpected)
        {
            var isValid = NotZeroAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта NotZero")]
        [InlineData(0u, false)]
        [InlineData(1u, true)]
        public void ValidateArgTest(uint arg, bool isValidExpected)
        {
            var isValid = NotZeroAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта NotZero")]
        [InlineData(0L, false)]
        [InlineData(1L, true)]
        public void ValidateArgTest(long arg, bool isValidExpected)
        {
            var isValid = NotZeroAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта NotZero")]
        [InlineData(0uL, false)]
        [InlineData(1uL, true)]
        public void ValidateArgTest(ulong arg, bool isValidExpected)
        {
            var isValid = NotZeroAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }
    }
}
