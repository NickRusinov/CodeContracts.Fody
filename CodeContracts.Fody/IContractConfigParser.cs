using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody
{
    public interface IContractConfigParser
    {
        ContractConfig Parse(string configString);
    }
}
