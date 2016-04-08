using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.TestAssembly.Jedis
{
    public interface IJedi
    {
        string Name { get; }

        string OrderRank { get; set; }
    }
}
