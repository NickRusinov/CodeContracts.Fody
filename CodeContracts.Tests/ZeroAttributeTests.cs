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
        [InlineData(1, false)]
        [InlineData(0, true)]
        public void ValidateArgTest(int arg, bool isValidExpected)
        {
            var isValid = ZeroAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Zero")]
        [InlineData(1u, false)]
        [InlineData(0u, true)]
        public void ValidateArgTest(uint arg, bool isValidExpected)
        {
            var isValid = ZeroAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Zero")]
        [InlineData(1L, false)]
        [InlineData(0L, true)]
        public void ValidateArgTest(long arg, bool isValidExpected)
        {
            var isValid = ZeroAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Zero")]
        [InlineData(1uL, false)]
        [InlineData(0uL, true)]
        public void ValidateArgTest(ulong arg, bool isValidExpected)
        {
            var isValid = ZeroAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }
    }
}
