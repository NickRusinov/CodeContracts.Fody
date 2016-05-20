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
        [NotNull]
        public string Name => "Plagueis";

        [True]
        public ISith Slave { get; set; }

        public bool JoinDarkSide()
        {
            return true;
        }
    }
}
