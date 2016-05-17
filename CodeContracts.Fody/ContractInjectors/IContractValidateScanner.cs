using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;

namespace CodeContracts.Fody.ContractInjectors
{
    public interface IContractValidateScanner
    {
        IEnumerable<ContractValidateDefinition> Scan(ContractDefinition contractDefinition);
    }
}
