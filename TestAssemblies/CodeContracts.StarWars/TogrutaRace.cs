using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.StarWars
{
    public class TogrutaRace : IRace
    {
        public Sentience Sentience { get; } = Sentience.Sentient;

        public long Population { get; set; } = 50000L;
    }
}
