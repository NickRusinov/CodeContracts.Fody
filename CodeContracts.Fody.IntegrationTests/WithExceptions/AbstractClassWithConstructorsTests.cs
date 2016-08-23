extern alias WithExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithExceptions::TestFoundations.IntegrationTests.Shared;
using Xunit;

namespace CodeContracts.Fody.IntegrationTests.WithExceptions
{
    public class AbstractClassWithConstructorsTests
    {
        [Fact]
        public void AbstractClassWithConstructorsTest()
        {
            ContractAssert.Success(() => new AbstractClassWithConstructorsImplementation());
        }

        [Fact]
        public void AbstractClassWithConstructorsWithObjectParameterTest()
        {
            ContractAssert.Success(() => new AbstractClassWithConstructorsImplementation(1));

            ContractAssert.Fail<AbstractClassWithConstructors, ArgumentException>(() => new AbstractClassWithConstructorsImplementation(1L));
        }
    }
}
