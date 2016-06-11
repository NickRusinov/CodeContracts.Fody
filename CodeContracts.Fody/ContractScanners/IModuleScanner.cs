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
    /// Scans custom contract attributes in a assembly
    /// </summary>
    public interface IModuleScanner
    {
        /// <summary>
        /// Scans custom contract attributes in a assembly
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <returns>Collection of all found contract attributes</returns>
        IEnumerable<ContractDefinition> Scan(ModuleDefinition moduleDefinition);
    }
}
