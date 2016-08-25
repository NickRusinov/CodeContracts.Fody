using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class StrictlyNegativeAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта StrictlyNegative")]
        [InlineData(1, false)]
        [InlineData(0, false)]
        [InlineData(-1, true)]
        public void ValidateArgIntTest(int arg, bool isValidExpected)
        {
            var isValid = StrictlyNegativeAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта StrictlyNegative")]
        [InlineData(1u, false)]
        [InlineData(0u, false)]
        public void ValidateArgUIntTest(uint arg, bool isValidExpected)
        {
            var isValid = StrictlyNegativeAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта StrictlyNegative")]
        [InlineData(1L, false)]
        [InlineData(0L, false)]
        [InlineData(-1L, true)]
        public void ValidateArgLongTest(long arg, bool isValidExpected)
        {
            var isValid = StrictlyNegativeAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        [Theory(DisplayName = "Проверка атрибута контракта StrictlyNegative")]
        [InlineData(1uL, false)]
        [InlineData(0uL, false)]
        public void ValidateArgULongTest(ulong arg, bool isValidExpected)
        {
            var isValid = StrictlyNegativeAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }
    }
}
