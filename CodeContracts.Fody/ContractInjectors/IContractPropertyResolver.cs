using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public interface IContractPropertyResolver
    {
        IParameterBuilder Resolve(ContractPropertyDefinition contractPropertyDefinition, MethodDefinition methodDefinition);
    }
}
