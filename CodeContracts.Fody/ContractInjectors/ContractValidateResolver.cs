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
    public class ContractValidateResolver : IContractValidateResolver
    {
        public ILookup<ContractValidateDefinition, IParameterBuilder> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            // todo -  алгоритм получения методов валидации контракта для метода
            return null;
        }
    }
}
