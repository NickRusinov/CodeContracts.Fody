using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    public interface IInstructionsBuilder
    {
        IEnumerable<Instruction> Build(ContractValidate contractValidate);
    }
}
