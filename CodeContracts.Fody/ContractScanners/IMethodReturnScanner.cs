using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractScanners
{
    /// <summary>
    /// Scans custom contract attributes in a method return value
    /// </summary>
    public interface IMethodReturnScanner
    {
        /// <summary>
        /// Scans custom contract attributes in a method return value
        /// </summary>
        /// <param name="methodReturnDefinition">Scanned method return value</param>
        /// <returns>Collection of all found contract attributes</returns>
        IEnumerable<ContractDefinition> Scan(MethodReturnType methodReturnDefinition);
    }
}
