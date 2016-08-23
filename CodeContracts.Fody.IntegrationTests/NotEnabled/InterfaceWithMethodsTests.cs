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
    public class InterfaceWithMethodsTests
    {
        [Fact]
        public void MethodWithoutAllTest()
        {
            ContractAssert.With<InterfaceWithMethodsImplementation>()
                .Success(sut => sut.MethodWithoutAll());
        }

        [Fact]
        public void MethodWithPositiveParameterTest()
        {
            ContractAssert.With<InterfaceWithMethodsImplementation>()
                .Success(sut => sut.MethodWithPositiveParameter(+ 1));

            ContractAssert.With<InterfaceWithMethodsImplementation>()
                .Success(sut => sut.MethodWithPositiveParameter(- 1));
        }

        [Fact]
        public void MethodWithPositiveAndNegativeParametersTest()
        {
            ContractAssert.With<InterfaceWithMethodsImplementation>()
                .Success(sut => sut.MethodWithPositiveAndNegativeParameters(+ 10, - 10));

            ContractAssert.With<InterfaceWithMethodsImplementation>()
                .Success(sut => sut.MethodWithPositiveAndNegativeParameters(- 10, - 10));

            ContractAssert.With<InterfaceWithMethodsImplementation>()
                .Success(sut => sut.MethodWithPositiveAndNegativeParameters(+ 10, + 10));
        }
    }
}
