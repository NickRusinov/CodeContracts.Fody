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
    public class InterfaceWithContractClassTests
    {
        [Fact]
        public void MethodWithContractClassTest()
        {
            ContractAssert.With<InterfaceWithContractClassImplementation>()
                .Success(sut => sut.FieldWithContractClassReturnValue = sut)
                .Success(sut => sut.MethodWithContractClass());

            ContractAssert.With<InterfaceWithContractClassImplementation>()
                .Success(sut => sut.FieldWithContractClassReturnValue = new InterfaceWithContractClassImplementation())
                .Fail(sut => sut.MethodWithContractClass());
        }

        [Fact]
        public void MethodWithContractAttributeTest()
        {
            ContractAssert.With<InterfaceWithContractClassImplementation>()
                .Success(sut => sut.FieldWithContractAttributeReturnValue = sut)
                .Success(sut => sut.MethodWithContractAttribute());

            ContractAssert.With<InterfaceWithContractClassImplementation>()
                .Success(sut => sut.FieldWithContractAttributeReturnValue = new InterfaceWithContractClassImplementation())
                .Success(sut => sut.MethodWithContractAttribute());
        }
    }
}
