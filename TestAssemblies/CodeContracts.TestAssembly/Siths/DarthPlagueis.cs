using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.TestAssembly.Siths
{
    public class DarthPlagueis : ISith
    {
        [NotNull]
        public string Name => "Plagueis";

        [True]
        public ISith Slave { get; set; }

        [My(Min = "$.value", Max = (short)42)]
        public bool JoinDarkSide()
        {
            return true;
        }
    }
}
