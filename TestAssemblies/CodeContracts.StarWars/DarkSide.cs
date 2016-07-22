using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.StarWars
{
    public class DarkSide : ForceSide
    {
        [Range(1, 10)]
        private readonly int bladeCount;

        public DarkSide(int bladeCount)
        {
            this.bladeCount = bladeCount;
        }
        
        public sealed override string Caption { get; } = "Dark";

        public sealed override string AlternativeCaption { get; set; }

        public override Lightsaber CreateLightsaber(LightsaberColor color)
        {
            return LightsaberFactory(bladeCount).FirstOrDefault(ls => ls.Color == color);
        }

        [return: NotNull]
        private static IEnumerable<Lightsaber> LightsaberFactory([Equals(1)] int bladeCount)
        {
            return LightsaberFactoryImplementation(bladeCount);
        }

        private static IEnumerable<Lightsaber> LightsaberFactoryImplementation(int bladeCount)
        {
            yield return new Lightsaber(LightsaberColor.Black, bladeCount);
            yield return new Lightsaber(LightsaberColor.Purple, bladeCount);

            for (var i = 0; i < 5; ++i)
                yield return new Lightsaber(LightsaberColor.Red, bladeCount);
        }
    }
}
