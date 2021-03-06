﻿extern alias WithExceptions;
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
    public class ConcreteClassWithFieldsTests
    {
        [Fact]
        public void FieldWithoutAllTest()
        {
            ContractAssert.With<ConcreteClassWithFields>()
                .Success(sut => sut.FieldWithoutAll = 7)
                .Success(sut => sut.InvokeInvariantMethod());
        }

        [Fact]
        public void FieldWithRange1And10Test()
        {
            ContractAssert.With<ConcreteClassWithFields>()
                .Success(sut => sut.FieldWithRange1And10 = 10)
                .Success(sut => sut.InvokeInvariantMethod());

            ContractAssert.With<ConcreteClassWithFields>()
                .Success(sut => sut.FieldWithRange1And10 = 12)
                .Fail(sut => sut.InvokeInvariantMethod());
        }

        [Fact]
        public void FieldWithRange10And100AndRange50And500Test()
        {
            ContractAssert.With<ConcreteClassWithFields>()
                .Success(sut => sut.FieldWithRange10And100AndRange50And500 = 99)
                .Success(sut => sut.InvokeInvariantMethod());

            ContractAssert.With<ConcreteClassWithFields>()
                .Success(sut => sut.FieldWithRange10And100AndRange50And500 = 101)
                .Fail(sut => sut.InvokeInvariantMethod());
        }
    }
}
