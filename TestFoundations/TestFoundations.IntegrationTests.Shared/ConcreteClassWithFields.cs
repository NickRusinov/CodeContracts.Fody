using System;
using System.Collections.Generic;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public class ConcreteClassWithFields
    {
        public int FieldWithoutAll;

        [Range(1, 10)]
        public int FieldWithRange1And10 = 5;

        [Range(10, 100), Range(50, 500)]
        public int FieldWithRange10And100AndRange50And500 = 75;

        public void InvokeInvariantMethod()
        {
            
        }
    }
}
