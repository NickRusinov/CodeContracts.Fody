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
    public class ModuleScanner : IModuleScanner
    {
        /// <summary>
        /// Scans custom contract attributes in a type
        /// </summary>
        private readonly ITypeScanner typeScanner;

        /// <summary>
        /// Initializes a new instance of calss <see cref="ModuleScanner"/>
        /// </summary>
        /// <param name="typeScanner">Scans custom contract attributes in a type</param>
        public ModuleScanner(ITypeScanner typeScanner)
        {
            Contract.Requires(typeScanner != null);

            this.typeScanner = typeScanner;
        }

        /// <inheritdoc/>
        public IEnumerable<ContractDefinition> Scan(ModuleDefinition moduleDefinition)
        {
            return from typeDefinition in moduleDefinition.Types
                   from contractDefinition in typeScanner.Scan(typeDefinition)
                   select contractDefinition;
        }
    }
}
