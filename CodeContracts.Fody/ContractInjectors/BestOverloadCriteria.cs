using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using static System.StringComparer;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Criteria that define can specified method overload calls with specified parameters
    /// </summary>
    public class BestOverloadCriteria : IBestOverloadCriteria
    {
        /// <inheritdoc/>
        public bool IsApply(MethodReference methodReference, IReadOnlyCollection<ParameterDefinition> parameterDefinitions)
        {
            return methodReference.Parameters.All(pd => parameterDefinitions.Select(ipd => ipd.Name).Contains(pd.Name, OrdinalIgnoreCase)) &&
                   parameterDefinitions.Where(pd => !pd.IsOptional).All(pd => methodReference.Parameters.Select(ipd => ipd.Name).Contains(pd.Name, OrdinalIgnoreCase)) &&
                   methodReference.Parameters.All(pd => parameterDefinitions.Single(ipd => OrdinalIgnoreCase.Equals(ipd.Name, pd.Name)).ParameterType.IsAssignable(pd.ParameterType));
        }
    }
}
