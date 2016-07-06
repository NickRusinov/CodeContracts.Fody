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
    /// Implements comparasion for <see cref="TypeReference"/> by its resolved <see cref="TypeDefinition"/>
    /// </summary>
    internal class TypeReferenceComparer : IEqualityComparer<TypeReference>
    {
        /// <summary>
        /// Singleton <see cref="TypeReferenceComparer"/>
        /// </summary>
        public static TypeReferenceComparer Instance { get; } = new TypeReferenceComparer();

        /// <inheridoc/>
        public bool Equals(TypeReference x, TypeReference y)
        {
            return object.Equals(x?.Resolve(), y?.Resolve());
        }

        /// <inheridoc/>
        public int GetHashCode(TypeReference obj)
        {
            return obj?.Resolve().GetHashCode() ?? 0;
        }
    }
}
