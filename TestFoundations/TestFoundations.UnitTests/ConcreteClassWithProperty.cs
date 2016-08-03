using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    public class ConcreteClassWithProperty
    {
        public Property Property { get; set; }

        [CustomContract]
        public Property PropertyWithAttribute { get; set; }
    }
}
