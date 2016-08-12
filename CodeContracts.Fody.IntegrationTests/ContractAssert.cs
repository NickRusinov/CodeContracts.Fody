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
        public static ContractAssert<TSut> Success<TSut>(Action<TSut> action)
            where TSut : new()
        {
            return new ContractAssert<TSut>(new TSut()).Success(action);
        }

        public static ContractAssert<TSut> Success<TSut>(Action<TSut> action, TSut sut)
        {
            return new ContractAssert<TSut>(sut).Success(action);
        }

        public static ContractAssert<TSut> Fail<TSut>(Action<TSut> action)
            where TSut : new()
        {
            return new ContractAssert<TSut>(new TSut()).Fail(action);
        }

        public static ContractAssert<TSut> Fail<TSut>(Action<TSut> action, TSut sut)
        {
            return new ContractAssert<TSut>(sut).Fail(action);
        }

        public static ContractAssert<TSut> With<TSut>()
            where TSut : new()
        {
            return new ContractAssert<TSut>(new TSut());
        }

        public static ContractAssert<TSut> With<TSut>(TSut sut)
        {
            return new ContractAssert<TSut>(sut);
        }

        public static void Get(this object o)
        {

        }
    }

    public class ContractAssert<TSut>
    {
        private readonly TSut sut;

        public ContractAssert(TSut sut)
        {
            this.sut = sut;
        }

        public ContractAssert<TSut> Success(Action<TSut> action)
        {
            var exception = Record.Exception(() => action.Invoke(sut));

            Assert.Null(exception);

            return this;
        }

        public ContractAssert<TSut> Fail(Action<TSut> action)
        {
            var exception = Record.Exception(() => action.Invoke(sut));

            Assert.NotNull(exception);
            Assert.Equal("ContractException", exception.GetType().Name);

            return this;
        }

        public ContractAssert<TSut> Fail<TException>(Action<TSut> action)
        {
            var exception = Record.Exception(() => action.Invoke(sut));

            Assert.NotNull(exception);
            Assert.IsType<TException>(exception);

            return this;
        }
    }
}
