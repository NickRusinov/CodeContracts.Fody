using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectResolvers
{
    public interface IInvariantResolver
    {
        MethodDefinition Resolve(InvariantDefinition invariantDefinition);
    }
}
