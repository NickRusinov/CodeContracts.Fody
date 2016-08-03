using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    public class ConcreteClassWithField
    {
        private readonly Field field;

        [CustomContract]
        private readonly Field fieldWithAttribute;
    }
}
