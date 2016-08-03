using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    public class ConcreteClassWithParametersAttributes
    {
        [CustomContractWithParameters(42, "value")]
        public void Method()
        {
            throw new NotImplementedException();
        }
    }
}
