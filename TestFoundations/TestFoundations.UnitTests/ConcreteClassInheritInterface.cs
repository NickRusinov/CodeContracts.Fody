using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    public class ConcreteClassInheritInterface : IInterface
    {
        public void Method()
        {
            throw new NotImplementedException();
        }

        public void MethodWithAttribute()
        {
            throw new NotImplementedException();
        }

        public void MethodWithReturnAttribute()
        {
            throw new NotImplementedException();
        }
    }
}
