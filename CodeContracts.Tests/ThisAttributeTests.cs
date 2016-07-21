using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class ThisAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта This")]
        [MemberData(nameof(ValidateSelfArgTestData))]
        public void ValidateSelfArgTest(object self, object arg, bool isValidExpected)
        {
            var isValid = ThisAttribute.Validate(self, arg);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateSelfArgTestData()
        {
            yield return new object[] { new object(), new object(), false };

            yield return new object[] { null, null, false };

            yield return new object[] { null, DBNull.Value, false };

            yield return new object[] { new Version(1, 1), null, false };

            var structInstance = new DateTime(256);
            yield return new object[] { structInstance, structInstance, false };

            var classInstance = new EventArgs();
            yield return new object[] { classInstance, classInstance, true };
        }
    }
}
