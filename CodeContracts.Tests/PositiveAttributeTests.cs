using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class PositiveAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта Positive")]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(1, true)]
        public void ValidateArgIntTest(int arg, bool isValidExpected)
        {
            var isValid = PositiveAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Positive")]
        [InlineData(0u, true)]
        [InlineData(1u, true)]
        public void ValidateArgUIntTest(uint arg, bool isValidExpected)
        {
            var isValid = PositiveAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Positive")]
        [InlineData(-1L, false)]
        [InlineData(0L, true)]
        [InlineData(1L, true)]
        public void ValidateArgLongTest(long arg, bool isValidExpected)
        {
            var isValid = PositiveAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта Positive")]
        [InlineData(0uL, true)]
        [InlineData(1uL, true)]
        public void ValidateArgULongTest(ulong arg, bool isValidExpected)
        {
            var isValid = PositiveAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }
    }
}
