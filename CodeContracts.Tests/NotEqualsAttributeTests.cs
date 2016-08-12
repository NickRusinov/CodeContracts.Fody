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
        [MemberData(nameof(ValidateArgToOfIntTestData))]
        public void ValidateArgToOfIntTest(int arg, int to, bool isValidExpected)
        {
            var isValid = NotEqualsAttribute.Validate(arg, to);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgToOfIntTestData()
        {
            yield return new object[] { 42, 42, false };

            yield return new object[] { 10, 20, true };
        }
    }
}
