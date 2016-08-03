using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    public interface IInterface
    {
        void Method();

        [CustomContract]
        void MethodWithAttribute();

        [return: CustomContract]
        void MethodWithReturnAttribute();
    }
}
