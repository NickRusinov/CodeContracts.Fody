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
    public class InterfaceWithPropertiesTests
    {
        [Fact]
        public void PropertyWithoutAllTest()
        {
            ContractAssert.With<InterfaceWithPropertiesImplementation>()
                .Success(sut => sut.PropertyWithoutAll = 127)
                .Success(sut => sut.PropertyWithoutAll.Get());
        }

        [Fact]
        public void PropertyWithRange0And500Test()
        {
            ContractAssert.With<InterfaceWithPropertiesImplementation>()
                .Success(sut => sut.PropertyWithRange0And500 = 127)
                .Success(sut => sut.PropertyWithRange0And500.Get());
        }

        [Fact]
        public void PropertyWithRange100And200GetOnlyTest()
        {
            ContractAssert.With<InterfaceWithPropertiesImplementation>()
                .Success(sut => sut.FieldWithRange100And200GetOnly = 127)
                .Success(sut => sut.PropertyWithRange100And200GetOnly.Get());

            ContractAssert.With<InterfaceWithPropertiesImplementation>()
                .Success(sut => sut.FieldWithRange100And200GetOnly = 255)
                .Success(sut => sut.PropertyWithRange100And200GetOnly.Get());
        }

        [Fact]
        public void PropertyWithRange100And200SetOnlyTest()
        {
            ContractAssert.With<InterfaceWithPropertiesImplementation>()
                .Success(sut => sut.PropertyWithRange100And200SetOnly = 127);

            ContractAssert.With<InterfaceWithPropertiesImplementation>()
                .Success(sut => sut.PropertyWithRange100And200SetOnly = 255);
        }
    }
}
