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
    /// Implements comparasion for <see cref="MethodReference"/> by its resolved <see cref="MethodDefinition"/>
    /// </summary>
    internal class MethodReferenceComparer : IEqualityComparer<MethodReference>
    {
        /// <summary>
        /// Singleton <see cref="MethodReferenceComparer"/>
        /// </summary>
        public static MethodReferenceComparer Instance { get; } = new MethodReferenceComparer();

        /// <inheridoc/>
        public bool Equals(MethodReference x, MethodReference y)
        {
            return object.Equals(x?.Resolve(), y?.Resolve());
        }

        /// <inheritdoc/>
        public int GetHashCode(MethodReference obj)
        {
            return obj?.Resolve().GetHashCode() ?? 0;
        }
    }
}
