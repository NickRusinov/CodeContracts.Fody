using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.StarWars
{
    public class BattleDroid : IDroid
    {
        [Equals(Sentience.NonSentient)]
        public Sentience Sentience { get; } = Sentience.SemiSentient;

        public long Population { get; set; } = 1000000L;

        public int Health { get; set; } = 100;

        public IDroid FixSelf()
        {
            if (Health <= 10)
                return new BattleDroid();

            return this;
        }
    }
}
