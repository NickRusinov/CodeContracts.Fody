using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractScanners
{
    /// <summary>
    /// Criteria that define that custom attribute is contract attribute
    /// </summary>
    public interface IContractCriteria
    {
        /// <summary>
        /// Define that custom attribute is contract attribute
        /// </summary>
        /// <param name="attribute">Custom attribute that will be checked</param>
        /// <returns>true, if custom attribute is contract attribute; otherwise, false</returns>
        bool IsContract(CustomAttribute attribute);
    }
}
