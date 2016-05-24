using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.TestAssembly.Siths
{
    public abstract class Sith : ISith
    {
        public abstract string Name { get; }

        [RedLightsaber]
        public virtual bool JoinDarkSide() => true;

        [ExtraRedLightsaber]
        public abstract void UseForceLightining(Force force);

        [True]
        public static Sith Create() => null;
    }
}
