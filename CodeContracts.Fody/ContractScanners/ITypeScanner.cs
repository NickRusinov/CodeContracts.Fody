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
    /// Scans custom contract attributes in a type
    /// </summary>
    public interface ITypeScanner
    {
        /// <summary>
        /// Scans custom contract attributes in a type
        /// </summary>
        /// <param name="typeDefinition">Scanned type</param>
        /// <returns>Collection of all found contract attributes</returns>
        IEnumerable<ContractDefinition> Scan(TypeDefinition typeDefinition);
    }
}
