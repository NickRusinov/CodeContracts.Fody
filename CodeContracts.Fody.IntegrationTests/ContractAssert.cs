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
        public static void Success(Action action)
        {
            var exception = Record.Exception(action);

            Assert.Null(exception);
        }

        public static void Fail(Action action)
        {
            var exception = Record.Exception(action);

            Assert.NotNull(exception);
            Assert.Equal("ContractException", exception.GetType().Name);
        }

        public static void Fail<TException>(Action action)
        {
            var exception = Record.Exception(action);

            Assert.NotNull(exception);
            Assert.IsType<TException>(exception);
        }

        public static void Success<TSut>(Action<TSut> action)
            where TSut : new()
        {
            Success(() => action(new TSut()));
        }

        public static void Success<TSut>(Action<TSut> action, TSut sut)
        {
            Success(() => action(sut));
        }

        public static void Fail<TSut>(Action<TSut> action)
            where TSut : new()
        {
            Fail(() => action(new TSut()));
        }

        public static void Fail<TSut>(Action<TSut> action, TSut sut)
        {
            Fail(() => action(sut));
        }

        public static void Get(this object o)
        {
            
        }
    }
}
