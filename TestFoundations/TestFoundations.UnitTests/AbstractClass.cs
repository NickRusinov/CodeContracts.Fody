using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    public abstract class AbstractClass
    {
        public abstract void AbstractMethod();

        public virtual void ConcreteMethod()
        {
            
        }
    }
}
