using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.BestOverloadResolvers
{
    /// <summary>
    /// Criteria that define can specified method overload calls with specified parameters
    /// </summary>
    public interface IBestOverloadCriteria
    {
        /// <summary>
        /// Defines can specified method overload calls with specified parameters
        /// </summary>
        /// <param name="methodReference">Specified method overload</param>
        /// <param name="parameterDefinitions">Specified collection of parameters</param>
        /// <returns>True then method match parameters; false otherwise</returns>
        bool IsApply(MethodReference methodReference, IReadOnlyCollection<ParameterDefinition> parameterDefinitions);
    }
}
