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
            return ContractAssert<TSut>.CreateSuccess(() => new TSut()).Success(action);
        }

        public static ContractAssert<TSut> Success<TSut>(Action<TSut> action, TSut sut)
        {
            return ContractAssert<TSut>.CreateSuccess(() => sut).Success(action);
        }

        public static ContractAssert<TSut> Success<TSut>(Func<TSut> sutFactory)
        {
            return ContractAssert<TSut>.CreateSuccess(sutFactory);
        }

        public static ContractAssert<TSut> Fail<TSut>(Action<TSut> action)
            where TSut : new()
        {
            return ContractAssert<TSut>.CreateSuccess(() => new TSut()).Fail(action);
        }

        public static ContractAssert<TSut> Fail<TSut>(Action<TSut> action, TSut sut)
        {
            return ContractAssert<TSut>.CreateSuccess(() => sut).Fail(action);
        }

        public static ContractAssert<TSut> Fail<TSut>(Func<TSut> sutFactory)
        {
            return ContractAssert<TSut>.CreateFail(sutFactory);
        }

        public static ContractAssert<TSut> With<TSut>()
            where TSut : new()
        {
            return ContractAssert<TSut>.CreateSuccess(() => new TSut());
        }

        public static ContractAssert<TSut> With<TSut>(TSut sut)
        {
            return ContractAssert<TSut>.CreateSuccess(() => sut);
        }

        public static void Get(this object o)
        {

        }
    }

    public class ContractAssert<TSut>
    {
        private readonly TSut sut;

        private ContractAssert(TSut sut)
        {
            this.sut = sut;
        }

        public static ContractAssert<TSut> CreateSuccess(Func<TSut> sutFactory)
        {
            var sut = default(TSut);
            var exception = Record.Exception(() => sut = sutFactory.Invoke());

            Assert.Null(exception);

            return new ContractAssert<TSut>(sut);
        }

        public static ContractAssert<TSut> CreateFail(Func<TSut> sutFactory)
        {
            var sut = default(TSut);
            var exception = Record.Exception(() => sut = sutFactory.Invoke());

            Assert.NotNull(exception);
            Assert.Equal("ContractException", exception.GetType().Name);

            return new ContractAssert<TSut>(sut);
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
