using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    public class ConcreteClassWithAttributes
    {
        [CustomContractMethodLevel]
        public void MethodWithMethodLevelAttribute()
        {
            throw new NotImplementedException();
        }

        [CustomContractClassLevel]
        public void MethodWithClassLevelAttribute()
        {
            throw new NotImplementedException();
        }
    }
}
