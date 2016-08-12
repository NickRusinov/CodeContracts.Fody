using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFoundations.UnitTests
{
    public class ConcreteClassWithAsyncs
    {
        public async Task<int> AsyncMethod()
        {
            return await Task.FromResult(0);
        }
    }
}
