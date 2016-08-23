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
    public class AbstractClassWithMethodsTests
    {
        [Fact]
        public void MethodWithoutAllTest()
        {
            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Success(sut => sut.MethodWithoutAll());
        }

        [Fact]
        public void MethodWithNullParameterTest()
        {
            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Success(sut => sut.MethodWithNullParameter(null));

            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Fail<ArgumentException>(sut => sut.MethodWithNullParameter(""));
        }

        [Fact]
        public void MethodWithNullAndNotNullParametersTest()
        {
            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Success(sut => sut.MethodWithNullAndNotNullParameters(null, ""));

            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Fail<ArgumentNullException>(sut => sut.MethodWithNullAndNotNullParameters(null, null));

            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Fail<ArgumentException>(sut => sut.MethodWithNullAndNotNullParameters("", ""));
        }

        [Fact]
        public void MethodAbstractWithoutAllTest()
        {
            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Success(sut => sut.MethodAbstractWithoutAll());
        }

        [Fact]
        public void MethodAbstractWithNullParameterTest()
        {
            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Success(sut => sut.MethodAbstractWithNullParameter(null));

            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Fail<ArgumentException>(sut => sut.MethodAbstractWithNullParameter(""));
        }

        [Fact]
        public void MethodAbstractWithNullAndNotNullParametersTest()
        {
            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Success(sut => sut.MethodAbstractWithNullAndNotNullParameters(null, ""));

            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Fail<ArgumentNullException>(sut => sut.MethodAbstractWithNullAndNotNullParameters(null, null));

            ContractAssert.With<AbstractClassWithMethodsImplementation>()
                .Fail<ArgumentException>(sut => sut.MethodAbstractWithNullAndNotNullParameters("", ""));
        }
    }
}
