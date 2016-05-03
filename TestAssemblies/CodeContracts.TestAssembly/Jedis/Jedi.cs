using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.TestAssembly.Jedis
{
    [ContractClass(typeof(JediContracts))]
    public abstract class Jedi : IJedi
    {
        public abstract string Name { get; }

        public virtual string OrderRank { get; set; } = "Padawan";

        public abstract void UseTheForce(Force force);
    }

    [ContractClassFor(typeof(Jedi))]
    internal abstract class JediContracts : Jedi
    {
        public override void UseTheForce(Force force)
        {
            Contract.Requires(force != null);

            throw new NotImplementedException();
        }
    }
}
