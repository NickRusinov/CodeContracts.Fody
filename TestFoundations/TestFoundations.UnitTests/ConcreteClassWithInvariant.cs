using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    public class ConcreteClassWithInvariant
    {
        [ContractInvariantMethod]
        private void InvariantMethod()
        {
            
        }
    }
}
