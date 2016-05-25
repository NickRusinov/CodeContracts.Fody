using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.TestAssembly.Siths
{
    public interface ISith
    {
        string Name { get; }

        [True]
        bool JoinDarkSide();
    }
}
