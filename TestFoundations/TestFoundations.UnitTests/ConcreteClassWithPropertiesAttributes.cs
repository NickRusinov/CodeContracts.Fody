using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    public class ConcreteClassWithPropertiesAttributes
    {
        [CustomContractWithProperties(X = 42, Y = "value", Z = typeof(void))]
        public void Method()
        {
            throw new NotImplementedException();
        }
    }
}
