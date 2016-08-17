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
    public class AbstractClassWithFieldsTests
    {
        [Fact]
        public void FieldWithoutAllTest()
        {
            ContractAssert.With<AbstractClassWithFieldsImplementation>()
                .Success(sut => sut.FieldWithoutAll = 3)
                .Success(sut => sut.InvokeInvariantMethod());
        }

        [Fact]
        public void FieldWithPositiveTest()
        {
            ContractAssert.With<AbstractClassWithFieldsImplementation>()
                .Success(sut => sut.FieldWithPositive = 3)
                .Success(sut => sut.InvokeInvariantMethod());

            ContractAssert.With<AbstractClassWithFieldsImplementation>()
                .Success(sut => sut.FieldWithPositive = -3)
                .Success(sut => sut.InvokeInvariantMethod());
        }

        [Fact]
        public void FieldWithPositiveAndRangeTest()
        {
            ContractAssert.With<AbstractClassWithFieldsImplementation>()
                .Success(sut => sut.FieldWithPositiveAndRange = 3)
                .Success(sut => sut.InvokeInvariantMethod());

            ContractAssert.With<AbstractClassWithFieldsImplementation>()
                .Success(sut => sut.FieldWithPositiveAndRange = 30)
                .Success(sut => sut.InvokeInvariantMethod());
        }
    }
}
