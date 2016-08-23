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
    public class ConcreteClassWithInvariantTests
    {
        [Fact]
        public void FieldNotNullInvariantTest()
        {
            ContractAssert.With<ConcreteClassWithInvariant>()
                .Success(sut => sut.FieldNotNullInvariant = new object())
                .Success(sut => sut.InvokeInvariantMethod());

            ContractAssert.With<ConcreteClassWithInvariant>()
                .Success(sut => sut.FieldNotNullInvariant = null)
                .Fail(sut => sut.InvokeInvariantMethod());
        }

        [Fact]
        public void FieldNotEquals77Test()
        {
            ContractAssert.With<ConcreteClassWithInvariant>()
                .Success(sut => sut.FieldNotEquals77 = 88)
                .Success(sut => sut.InvokeInvariantMethod());

            ContractAssert.With<ConcreteClassWithInvariant>()
                .Success(sut => sut.FieldNotEquals77 = 77)
                .Fail(sut => sut.InvokeInvariantMethod());
        }
    }
}
