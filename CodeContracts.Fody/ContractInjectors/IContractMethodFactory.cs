using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public interface IContractMethodFactory
    {
        IInstructionsBuilder Create(TypeDefinition typeDefinition, string message);
    }
}
