using System;
using System.Collections.Generic;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public abstract class AbstractClassWithProperties
    {
        public long PropertyWithoutAll { get; set; }

        [Negative]
        public long PropertyWithNegative { get; set; }

        public long FieldWithEquals1000GetOnly;
        [Equals(1000)]
        public long PropertyWithEquals1000GetOnly
        {
            get { return FieldWithEquals1000GetOnly; }
        }

        public long FieldWithNotZeroSetOnly;
        [NotZero]
        public long PropertyWithNotZeroSetOnly
        {
            set { FieldWithNotZeroSetOnly = value; }
        }

        public abstract long PropertyAbstractWithoutAll { get; set; }

        [Positive]
        public abstract long PropertyAbstractWithPositive { get; set; }

        [Equals(1000)]
        public abstract long PropertyAbstractWithEquals1000GetOnly { get; }

        [NotZero]
        public abstract long PropertyAbstractWithNotZeroSetOnly { set; }
    }

    public class AbstractClassWithPropertiesImplementation : AbstractClassWithProperties
    {
        public override long PropertyAbstractWithoutAll { get; set; }

        public override long PropertyAbstractWithPositive { get; set; }

        public long FieldAbstractWithEquals1000GetOnly;
        public override long PropertyAbstractWithEquals1000GetOnly
        {
            get { return FieldAbstractWithEquals1000GetOnly; }
        }

        public long FieldAbstractWithNotZeroSetOnly;
        public override long PropertyAbstractWithNotZeroSetOnly
        {
            set { FieldAbstractWithNotZeroSetOnly = value; }
        }
    }
}
