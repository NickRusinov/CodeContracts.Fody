using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.Internal
{
    internal class MethodReferenceComparer : IEqualityComparer<MethodReference>
    {
        public static MethodReferenceComparer Instance { get; } = new MethodReferenceComparer();

        public bool Equals(MethodReference x, MethodReference y)
        {
            return object.Equals(x?.Resolve(), y?.Resolve());
        }

        public int GetHashCode(MethodReference obj)
        {
            return obj?.Resolve().GetHashCode() ?? 0;
        }
    }
}
