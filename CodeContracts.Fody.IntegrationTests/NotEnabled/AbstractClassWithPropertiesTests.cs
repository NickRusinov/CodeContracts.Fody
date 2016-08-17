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
    public class AbstractClassWithPropertiesTests
    {
        [Fact]
        public void PropertyWithoutAllTest()
        {
            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.PropertyWithoutAll = 55)
                .Success(sut => sut.PropertyWithoutAll.Get());
        }

        [Fact]
        public void PropertyWithNegativeTest()
        {
            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.PropertyWithNegative = - 55)
                .Success(sut => sut.PropertyWithNegative.Get());

            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.PropertyWithNegative = 55);
        }

        [Fact]
        public void PropertyWithEquals1000GetOnlyTest()
        {
            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.FieldWithEquals1000GetOnly = 1000)
                .Success(sut => sut.PropertyWithEquals1000GetOnly.Get());

            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.FieldWithEquals1000GetOnly = 999)
                .Success(sut => sut.PropertyWithEquals1000GetOnly.Get());
        }

        [Fact]
        public void PropertyWithNotZeroSetOnlyTest()
        {
            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.PropertyWithNotZeroSetOnly = 55);

            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.PropertyWithNotZeroSetOnly = 0);
        }

        [Fact]
        public void PropertyAbstractWithoutAllTest()
        {
            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.PropertyAbstractWithoutAll = 55)
                .Success(sut => sut.PropertyAbstractWithoutAll.Get());
        }

        [Fact]
        public void PropertyAbstractWithPositiveTest()
        {
            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.PropertyAbstractWithPositive = 55)
                .Success(sut => sut.PropertyAbstractWithPositive.Get());

            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.PropertyAbstractWithPositive = - 55);
        }

        [Fact]
        public void PropertyAbstractWithEquals1000GetOnlyTest()
        {
            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.FieldAbstractWithEquals1000GetOnly = 1000)
                .Success(sut => sut.PropertyAbstractWithEquals1000GetOnly.Get());

            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.FieldAbstractWithEquals1000GetOnly = 999)
                .Success(sut => sut.PropertyAbstractWithEquals1000GetOnly.Get());
        }

        [Fact]
        public void PropertyAbstractWithNotZeroSetOnlyTest()
        {
            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.PropertyAbstractWithNotZeroSetOnly = 55);

            ContractAssert.With<AbstractClassWithPropertiesImplementation>()
                .Success(sut => sut.PropertyAbstractWithNotZeroSetOnly = 0);
        }
    }
}
