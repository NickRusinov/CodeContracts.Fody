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
    public class ModuleScanner : IModuleScanner
    {
        private readonly ITypeScanner typeScanner;

        public ModuleScanner(ITypeScanner typeScanner)
        {
            Contract.Requires(typeScanner != null);

            this.typeScanner = typeScanner;
        }

        public IEnumerable<ContractDefinition> Scan(ModuleDefinition moduleDefinition)
        {
            return
                from typeDefinition in moduleDefinition.Types
                from contractDefinition in typeScanner.Scan(typeDefinition)
                select contractDefinition;
        }
    }
}
