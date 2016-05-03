using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.TestAssembly.Jedis
{
    [ContractClass(typeof(IJediContracts))]
    public interface IJedi
    {
        string Name { get; }

        string OrderRank { get; set; }
    }

    [ContractClassFor(typeof(IJedi))]
    internal abstract class IJediContracts : IJedi
    {
        public string Name
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

                throw new NotImplementedException();
            }
        }

        public string OrderRank
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
