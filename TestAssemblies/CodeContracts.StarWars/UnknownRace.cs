using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.StarWars
{
    public class UnknownRace : IRace
    {
        public Sentience Sentience { get; } = (Sentience)(- 1);

        public long Population { get; set; } = - 1L;
    }
}
