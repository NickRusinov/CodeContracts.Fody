using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.TestAssembly.Jedis
{
    public class QuiGonJinn : IJedi
    {
        public string Name => "Qui-Gon Jinn";

        public string OrderRank { get; set; } = "Jedi Master";
    }
}
