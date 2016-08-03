using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    public class ConcreteClass
    {
        public static void StaticMethod()
        {
            throw new NotImplementedException();
        }

        public void Method()
        {
            throw new NotImplementedException();
        }
        
        [CustomContract]
        public void MethodWithAttribute()
        {
            throw new NotImplementedException();
        }

        [NoCustomContract]
        public void MethodWithNoContractAttribute()
        {
            throw new NotImplementedException();
        }

        [return: CustomContract]
        public void MethodWithReturnAttribute()
        {
            throw new NotImplementedException();
        }

        public void MethodWithParameter([CustomContract] Parameter parameter)
        {
            throw new NotImplementedException();
        }
    }
}
