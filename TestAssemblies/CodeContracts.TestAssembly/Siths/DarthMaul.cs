using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void KillJedi(IJedi jedi, double revenge)
        {

        }

        public string Name => "Maul";
    }
}
