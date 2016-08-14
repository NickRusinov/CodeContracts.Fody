using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class RegexAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта Negative")]
        [InlineData("aaa", "^\\d{3}$", false)]
        [InlineData("111", "^\\d{3}$", true)]
        public void ValidateArgRegexTest(string arg, string regex, bool isValidExpected)
        {
            var isValid = RegexAttribute.Validate(arg, regex);

            Assert.Equal(isValidExpected, isValid);
        }
    }
}
