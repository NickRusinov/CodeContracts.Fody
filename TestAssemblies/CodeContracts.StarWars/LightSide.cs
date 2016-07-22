using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.StarWars
{
    public class LightSide : ForceSide
    {
        public LightSide([NotNull] string caption = "Light", [NotNull] string alternationCaption = "Ashla")
        {
            Caption = caption;
            AlternativeCaption = alternationCaption;
        }

        public sealed override string Caption { get; }

        public sealed override string AlternativeCaption { get; set; }

        public override Lightsaber CreateLightsaber(LightsaberColor color)
        {
            if (color == LightsaberColor.Red)
                return null;

            return new Lightsaber(color, 1);
        }
    }
}
