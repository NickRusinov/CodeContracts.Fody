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

        public virtual bool JoinDarkSide() => true;

        public abstract void UseForceLightining(Force force);

        [True]
        public static Sith Create() => null;
    }
}
