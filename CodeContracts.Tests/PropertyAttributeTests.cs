using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Tests
{
    public class PropertyAttributeTests
    {
        [Theory(DisplayName = "Проверка атрибута контракта Property")]
        [MemberData(nameof(ValidateArgTestData))]
        public void ValidateArgTest(Expression arg, bool isValidExpected)
        {
            var isValid = PropertyAttribute.Validate(arg);

            Assert.Equal(isValidExpected, isValid);
        }

        private static IEnumerable<object[]> ValidateArgTestData()
        {
            yield return new object[] { (Expression<Func<object, string>>)((object o) => o.ToString()), false };
            yield return new object[] { (Expression<Func<DateTime, DateTime>>)((DateTime dt) => dt.Date.Date), false };

            yield return new object[] { (Expression<Func<DateTime, DateTime>>)((DateTime dt) => dt.Date), true };
        }
    }
}
