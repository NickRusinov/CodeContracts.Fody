using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ILProcessorResolvers
{
    public interface IEnsuresResolver
    {
        ILProcessor Resolve(EnsuresDefinition ensuresDefinition);
    }
}
