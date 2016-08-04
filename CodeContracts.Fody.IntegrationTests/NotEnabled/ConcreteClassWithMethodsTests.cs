extern alias NotEnabled;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotEnabled::TestFoundations.IntegrationTests.Shared;
using Xunit;

namespace CodeContracts.Fody.IntegrationTests.NotEnabled
{
    public class ConcreteClassWithMethodsTests
    {
        [Fact]
        public void MethodWithoutAllTest()
        {
            ContractAssert.Success<ConcreteClassWithMethods>(sut => sut.MethodWithoutAll());
        }

        [Fact]
        public void MethodWithNotNullParameterTest()
        {
            ContractAssert.Success<ConcreteClassWithMethods>(sut => sut.MethodWithNotNullParameter(new object()));
            ContractAssert.Success<ConcreteClassWithMethods>(sut => sut.MethodWithNotNullParameter(null));
        }

        [Fact]
        public void MethodWithTrueAndFalseParametersTest()
        {
            ContractAssert.Success<ConcreteClassWithMethods>(sut => sut.MethodWithTrueAndFalseParameters(true, false));
            ContractAssert.Success<ConcreteClassWithMethods>(sut => sut.MethodWithTrueAndFalseParameters(false, false));
            ContractAssert.Success<ConcreteClassWithMethods>(sut => sut.MethodWithTrueAndFalseParameters(true, true));
        }
    }
}
