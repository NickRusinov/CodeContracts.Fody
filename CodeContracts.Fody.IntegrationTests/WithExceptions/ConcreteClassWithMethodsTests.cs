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
    public class ConcreteClassWithMethodsTests
    {
        [Fact]
        public void MethodWithoutAllTest()
        {
            ContractAssert.With<ConcreteClassWithMethods>()
                .Success(sut => sut.MethodWithoutAll());
        }

        [Fact]
        public void MethodWithNotNullParameterTest()
        {
            ContractAssert.With<ConcreteClassWithMethods>()
                .Success(sut => sut.MethodWithNotNullParameter(new object()));

            ContractAssert.With<ConcreteClassWithMethods>()
                .Fail<ArgumentNullException>(sut => sut.MethodWithNotNullParameter(null));
        }

        [Fact]
        public void MethodWithTrueAndFalseParametersTest()
        {
            ContractAssert.With<ConcreteClassWithMethods>()
                .Success(sut => sut.MethodWithTrueAndFalseParameters(true, false));

            ContractAssert.With<ConcreteClassWithMethods>()
                .Fail<ArgumentException>(sut => sut.MethodWithTrueAndFalseParameters(false, false));

            ContractAssert.With<ConcreteClassWithMethods>()
                .Fail<ArgumentException>(sut => sut.MethodWithTrueAndFalseParameters(true, true));
        }
    }
}
