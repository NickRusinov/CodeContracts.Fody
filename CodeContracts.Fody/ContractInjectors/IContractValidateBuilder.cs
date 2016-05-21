using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    public interface IContractValidateBuilder
    {
        IEnumerable<Instruction> Build(ContractValidateDefinition contractValidateDefinition, IEnumerable<Instruction> instructions);
    }
}
