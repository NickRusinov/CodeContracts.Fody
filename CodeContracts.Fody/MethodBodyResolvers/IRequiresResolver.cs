using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.MethodBodyResolvers
{
    public interface IRequiresResolver
    {
        MethodBody Resolve(RequiresDefinition requiresDefinition);
    }
}
