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
        private readonly Lightsaber lightsaber;

        public DarthMaul(Lightsaber lightsaber)
        {
            this.lightsaber = lightsaber;
        }

        [return: Contract]
        public bool KillJedi([Contract] IJedi jedi, [NoContract] double revenge)
        {
            return revenge > 100.0;
        }

        [return: NoContract]
        public bool JoinDarkSide()
        {
            return true;
        }

        public string Name => "Maul";
    }
}
