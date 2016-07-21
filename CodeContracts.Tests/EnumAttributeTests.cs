using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class EnumAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта Enum")]
        [MemberData(nameof(ValidateArgTestData))]
        public void ValidateArgTest(Enum arg, bool isValidExpected)
        {
            var isValid = EnumAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgTestData()
        {
            yield return new object[] { null, false };

            yield return new object[] { (StringSplitOptions)1000, false };
            
            yield return new object[] { GCCollectionMode.Forced, true };
        }
    }
}
