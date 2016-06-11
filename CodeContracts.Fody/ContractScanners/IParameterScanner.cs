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
    /// Scans custom contract attributes in a parameter of method
    /// </summary>
    public interface IParameterScanner
    {
        /// <summary>
        /// Scans custom contract attributes in a parameter of method
        /// </summary>
        /// <param name="parameterDefinition">Scanned parameter of method</param>
        /// <returns>Collection of all found contract attributes</returns>
        IEnumerable<ContractDefinition> Scan(ParameterDefinition parameterDefinition);
    }
}
