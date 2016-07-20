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
        public static void Fail<T>(Func<T> constructor, params Action<T>[] actions)
        {
            var exception = Record.Exception(() =>
            {
                var instance = constructor.Invoke();
                Array.ForEach(actions, a => a.Invoke(instance));
            });

            Assert.NotNull(exception);
            Assert.Equal("ContractException", exception.GetType().Name);
        }

        public static void Success<T>(Func<T> constructor, params Action<T>[] actions)
        {
            var exception = Record.Exception(() =>
            {
                var instance = constructor.Invoke();
                Array.ForEach(actions, a => a.Invoke(instance));
            });

            Assert.Null(exception);
        }

        public static void Get(this object o)
        {
            
        }
    }
}
