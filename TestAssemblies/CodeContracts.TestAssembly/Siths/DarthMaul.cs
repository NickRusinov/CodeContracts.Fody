using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.TestAssembly.Contracts;
using CodeContracts.TestAssembly.Jedis;

namespace CodeContracts.TestAssembly.Siths
{
    public class DarthMaul : ISith
    {
        [True]
        private readonly Lightsaber lightsaber;

        [NoContract]
        private readonly ISith master;

        public DarthMaul(Lightsaber lightsaber, ISith master)
        {
            this.lightsaber = lightsaber;
            this.master = master;
        }

        [NoContract]
        [return: True]
        public bool KillJedi([NotNull] IJedi jedi, [NoContract] double revenge)
        {
            return revenge > 100.0;
        }

        [Range("$parameter", 5ul, Min = "$.value", Max = (short)42)]
        [return: NoContract]
        public bool JoinDarkSide()
        {
            return true;
        }

        public string Name => "Maul";
    }
}
