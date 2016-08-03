using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    [ContractClass(typeof(AbstractClassWithContractClassContracts))]
    public abstract class AbstractClassWithContractClass
    {
        public abstract void AbstractMethod();

        public virtual void ConcreteMethod()
        {
            
        }
    }

    [ContractClassFor(typeof(AbstractClassWithContractClass))]
    internal abstract class AbstractClassWithContractClassContracts : AbstractClassWithContractClass
    {
        public override void AbstractMethod()
        {
            throw new NotImplementedException();
        }
    }
}
