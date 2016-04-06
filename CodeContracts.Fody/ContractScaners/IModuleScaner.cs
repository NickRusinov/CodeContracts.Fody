using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractScaners
{
    public interface IModuleScaner
    {
        IEnumerable<ContractDefinition> Scan(ModuleDefinition moduleDefinition);
    }
}
