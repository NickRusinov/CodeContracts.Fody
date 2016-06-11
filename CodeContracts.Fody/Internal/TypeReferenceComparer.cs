using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.Internal
{
    internal class TypeReferenceComparer : IEqualityComparer<TypeReference>
    {
        public static TypeReferenceComparer Instance { get; } = new TypeReferenceComparer();

        public bool Equals(TypeReference x, TypeReference y)
        {
            return object.Equals(x?.Resolve(), y?.Resolve());
        }

        public int GetHashCode(TypeReference obj)
        {
            return obj?.Resolve().GetHashCode() ?? 0;
        }
    }
}
