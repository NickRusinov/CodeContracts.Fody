﻿extern alias NotClean;
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
    public class ConcreteClassWithPropertiesTests
    {
        [Fact]
        public void PropertyWithoutAllTest()
        {
            ContractAssert.With<ConcreteClassWithProperties>()
                .Success(sut => sut.PropertyWithoutAll = new object())
                .Success(sut => sut.PropertyWithoutAll.Get());
        }

        [Fact]
        public void PropertyWithNotZeroTest()
        {
            ContractAssert.With<ConcreteClassWithProperties>()
                .Success(sut => sut.PropertyWithNotZero = 42)
                .Success(sut => sut.PropertyWithNotZero.Get());

            ContractAssert.With<ConcreteClassWithProperties>()
                .Fail(sut => sut.PropertyWithNotZero = 0);
        }

        [Fact]
        public void PropertyWithNotZeroGetOnlyTest()
        {
            ContractAssert.With<ConcreteClassWithProperties>()
                .Success(sut => sut.PropertyWithNotZeroGetOnlyField = 42)
                .Success(sut => sut.PropertyWithNotZeroGetOnly.Get());

            ContractAssert.With<ConcreteClassWithProperties>()
                .Success(sut => sut.PropertyWithNotZeroGetOnlyField = 0)
                .Fail(sut => sut.PropertyWithNotZeroGetOnly.Get());
        }

        [Fact]
        public void PropertyWithZeroSetOnlyTest()
        {
            ContractAssert.With<ConcreteClassWithProperties>()
                .Success(sut => sut.PropertyWithZeroSetOnly = 0);

            ContractAssert.With<ConcreteClassWithProperties>()
                .Fail(sut => sut.PropertyWithZeroSetOnly = 42);
        }
    }
}
