using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeContracts.Fody.IntegrationTests
{
    public static class ContractAssert
    {
        public static void Fail(Action action)
        {
            var exception = Assert.ThrowsAny<Exception>(action);
            Assert.Equal("ContractException", exception.GetType().Name);
        }

        public static void Success(Action action)
        {
            action.Invoke();
            Assert.True(true);
        }

        public static void Fail(Func<object> func) => Fail(() => { func(); });

        public static void Success(Func<object> func) => Success(() => { func(); });
    }
}
