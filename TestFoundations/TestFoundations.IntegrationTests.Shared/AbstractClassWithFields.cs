using System;
using System.Collections.Generic;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public abstract class AbstractClassWithFields
    {
        public short FieldWithoutAll;

        [Positive]
        public short FieldWithPositive = 1;

        [Positive, Range(-10, 10)]
        public short FieldWithPositiveAndRange = 5;

        public void InvokeInvariantMethod()
        {

        }
    }

    public class AbstractClassWithFieldsImplementation : AbstractClassWithFields
    {
        
    }
}
