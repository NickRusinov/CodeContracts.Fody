using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    [ContractClass(typeof(IInterfaceWithContractClassContracts))]
    public interface IInterfaceWithContractClass
    {
        void Method();
    }

    [ContractClassFor(typeof(IInterfaceWithContractClass))]
    internal abstract class IInterfaceWithContractClassContracts : IInterfaceWithContractClass
    {
        public void Method()
        {
            throw new NotImplementedException();
        }
    }
}
