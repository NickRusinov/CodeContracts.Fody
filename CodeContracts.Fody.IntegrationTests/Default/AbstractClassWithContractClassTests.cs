extern alias Default;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Default::TestFoundations.IntegrationTests.Shared;
using Xunit;

namespace CodeContracts.Fody.IntegrationTests.Default
{
    public class AbstractClassWithContractClassTests
    {
        [Fact]
        public void MethodAbstractWithContractClassTest()
        {
            ContractAssert.With<AbstractClassWithContratcClassImplementation>()
                .Success(sut => sut.MethodAbstractWithContractClass("1"));

            ContractAssert.With<AbstractClassWithContratcClassImplementation>()
                .Fail(sut => sut.MethodAbstractWithContractClass("a"));
        }

        [Fact]
        public void MethodAbstractWithContractAttributeTest()
        {
            ContractAssert.With<AbstractClassWithContratcClassImplementation>()
                .Success(sut => sut.MethodAbstractWithContractAttribute("1"));

            ContractAssert.With<AbstractClassWithContratcClassImplementation>()
                .Fail(sut => sut.MethodAbstractWithContractAttribute("a"));
        }
    }
}
