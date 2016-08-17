extern alias NotClean;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotClean::TestFoundations.IntegrationTests.Shared;
using Xunit;

namespace CodeContracts.Fody.IntegrationTests.NotClean
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

            ContractAssert.Fail(() => new AbstractClassWithConstructorsImplementation(1L));
        }
    }
}
