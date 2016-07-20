using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.StarWars
{
    public abstract class ForceSide
    {
        [NotNull]
        public abstract string Caption { get; }

        [NotNull]
        public abstract string AlternativeCaption { get; set; }

        [return: NotNull]
        public abstract Lightsaber CreateLightsaber([Enum] LightsaberColor color);
    }
}
