using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.TestAssembly
{
    public class Lightsaber
    {
        public Lightsaber(LightsaberColor color, int bladeCount)
        {
            Color = color;
            BladeCount = bladeCount;
        }

        [RedLightsaber, BlueLightsaber]
        public LightsaberColor Color { get; }

        public int BladeCount { get; }
    }
}
