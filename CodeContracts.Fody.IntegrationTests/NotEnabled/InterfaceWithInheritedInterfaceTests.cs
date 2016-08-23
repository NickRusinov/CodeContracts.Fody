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
    public class InterfaceWithInheritedInterfaceTests
    {
        [Fact]
        public void MethodSumIntTest()
        {
            ContractAssert.With<InterfaceWithInheritedInterfaceImplementation>()
                .Success(sut => sut.MethodSumInt(42, 58));

            ContractAssert.With<InterfaceWithInheritedInterfaceImplementation>()
                .Success(sut => sut.MethodSumInt(42, 59));
        }

        [Fact]
        public void MethodSumLongTest()
        {
            ContractAssert.With<InterfaceWithInheritedInterfaceImplementation>()
                .Success(sut => sut.MethodSumLong(42, 58));

            ContractAssert.With<InterfaceWithInheritedInterfaceImplementation>()
                .Success(sut => sut.MethodSumLong(42, 59));
        }
    }
}
