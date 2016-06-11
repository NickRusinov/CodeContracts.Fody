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
    /// Scans custom contract attributes in a property
    /// </summary>
    public interface IPropertyScanner
    {
        /// <summary>
        /// Scans custom contract attributes in a property
        /// </summary>
        /// <param name="propertyDefinition">Scanned property</param>
        /// <returns>Collection of all found contract attributes</returns>
        IEnumerable<ContractDefinition> Scan(PropertyDefinition propertyDefinition);
    }
}
