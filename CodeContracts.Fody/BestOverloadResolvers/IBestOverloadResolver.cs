using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.BestOverloadResolvers
{
    /// <summary>
    /// Resolves method that is a best overload for specified parameters
    /// </summary>
    public interface IBestOverloadResolver
    {
        /// <summary>
        /// Resolves method that is a best overload for specified parameters among specified methods
        /// </summary>
        /// <param name="methodReferences">Collection of method among that resolves best overload</param>
        /// <param name="parameterDefinitions">Collection of method's parameters</param>
        /// <returns>Best overload among specified methods</returns>
        MethodReference Resolve(IReadOnlyCollection<MethodReference> methodReferences, IReadOnlyCollection<ParameterDefinition> parameterDefinitions);
    }
}
