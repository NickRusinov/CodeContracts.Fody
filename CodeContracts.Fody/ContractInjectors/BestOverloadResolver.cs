using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class BestOverloadResolver : IBestOverloadResolver
    {
        public MethodDefinition Resolve(IEnumerable<MethodDefinition> methodDefinitions, IEnumerable<ParameterDefinition> parameterDefinitions)
        {
            // todo - алгоритм выбора наилучшей перегрузки метода
            return methodDefinitions.FirstOrDefault();
        }
    }
}
