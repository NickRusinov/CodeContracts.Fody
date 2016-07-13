using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.BestOverloadResolvers
{
    /// <summary>
    /// Criteria that define can specified method overload calls with specified parameters
    /// </summary>
    public class BestOverloadCriteria : IBestOverloadCriteria
    {
        /// <inheritdoc/>
        public bool IsApply(MethodReference methodReference, IReadOnlyCollection<ParameterDefinition> parameterDefinitions)
        {
            return methodReference.Parameters.All(pd => parameterDefinitions.Select(ipd => ipd.Name).Contains(pd.Name, StringComparer.OrdinalIgnoreCase)) &&
                   parameterDefinitions.Where(pd => !pd.IsOptional).All(pd => methodReference.Parameters.Select(ipd => ipd.Name).Contains(pd.Name, StringComparer.OrdinalIgnoreCase)) &&
                   methodReference.Parameters.All(pd => parameterDefinitions.Single(ipd => StringComparer.OrdinalIgnoreCase.Equals(ipd.Name, pd.Name)).ParameterType.IsAssignable(pd.ParameterType));
        }
    }
}
