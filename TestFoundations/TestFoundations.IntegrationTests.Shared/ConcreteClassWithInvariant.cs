using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public class ConcreteClassWithInvariant
    {
        public object FieldNotNullInvariant = new object();

        [NotEquals(77)]
        public int FieldNotEquals77;

        public void InvokeInvariantMethod()
        {
            
        }

        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            Contract.Invariant(FieldNotNullInvariant != null);
        }
    }
}
