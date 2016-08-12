using System;
using System.Collections.Generic;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public class ConcreteClassWithProperties
    {
        public object PropertyWithoutAll { get; set; }

        [NotZero]
        public int PropertyWithNotZero { get; set; }

        public int PropertyWithNotZeroGetOnlyField;
        [NotZero]
        public int PropertyWithNotZeroGetOnly
        {
            get { return PropertyWithNotZeroGetOnlyField; }
        }

        public int PropertyWithZeroSetOnlyField;
        [Zero]
        public int PropertyWithZeroSetOnly
        {
            set { PropertyWithZeroSetOnlyField = value; }
        }
    }
}
