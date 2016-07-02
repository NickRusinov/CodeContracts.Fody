using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody
{
    /// <summary>
    /// Parses string to object that represent configuration of code contracts fody addin
    /// </summary>
    public interface IContractConfigParser
    {
        /// <summary>
        /// Pasres string to object that represent configuration of code contracts fody addin
        /// </summary>
        /// <param name="configString">String that will be parsed</param>
        /// <returns>Object that represent configuration of code contracts fody addin</returns>
        ContractConfig Parse(string configString);
    }
}
