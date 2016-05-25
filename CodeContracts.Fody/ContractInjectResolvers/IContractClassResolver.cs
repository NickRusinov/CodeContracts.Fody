using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectResolvers
{
    public interface IContractClassResolver
    {
        TypeDefinition Resolve(TypeDefinition typeDefinition, MethodDefinition methodDefinition);
    }
}
