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
        public Lightsaber([Enum] LightsaberColor color, int bladeCount)
        {
            Color = color;
            BladeCount = bladeCount;
        }
        
        public LightsaberColor Color { get; set; }
        
        public int BladeCount { get; set; }
    }
}
