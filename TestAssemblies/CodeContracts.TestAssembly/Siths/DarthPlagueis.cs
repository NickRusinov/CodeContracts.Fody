using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.TestAssembly.Contracts;

namespace CodeContracts.TestAssembly.Siths
{
    public class DarthPlagueis : ISith
    {
        [Contract]
        public string Name => "Plagueis";

        [Contract]
        public ISith Slave { get; set; }

        public bool JoinDarkSide()
        {
            return true;
        }
    }
}
