using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.StarWars
{
    public class Lightsaber
    {
        public Lightsaber([Enum] LightsaberColor color, [Range(1, 2)] int bladeCount)
        {
            Color = color;
            BladeCount = bladeCount;
        }
        
        [Enum]
        public LightsaberColor Color { get; set; }
        
        [Range((short)1, (short)2)]
        public int BladeCount { get; set; }
    }
}
