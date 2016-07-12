using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.Internal
{
    /// <summary>
    /// Contains extension methods for <see cref="CustomAttribute"/>
    /// </summary>
    internal static class CustomAttributeExtensions
    {
        /// <summary>
        /// Gets constructor's arguments of attribute as collection of <see cref="CustomAttributeNamedArgument"/>
        /// </summary>
        /// <param name="customAttribute">Custom attribute</param>
        /// <returns>Collection of named constructor's arguments</returns>
        public static IEnumerable<CustomAttributeNamedArgument> GetNamedConstructorArguments(this CustomAttribute customAttribute)
        {
            return customAttribute.ConstructorArguments.Zip(customAttribute.Constructor.Resolve().Parameters, (caa, pd) => new CustomAttributeNamedArgument(pd.Name, caa));
        }
    }
}
