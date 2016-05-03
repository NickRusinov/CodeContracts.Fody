using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace CodeContracts.Fody.Internal
{
    internal static class IsOverrideUtils
    {
        public static bool IsOverride(this MethodDefinition method, MethodReference overridden)
        {
            Contract.Requires(method != null);
            Contract.Requires(overridden != null);

            return method.Overrides.Contains(overridden) ||
                method.GetOriginalBaseMethod().Equals(overridden) ||
                method.Name.Equals(overridden.Name) &&
                overridden.DeclaringType.Resolve().IsInterface &&
                method.DeclaringType.Interfaces.Contains(overridden.DeclaringType) &&
                !method.DeclaringType.Methods.SelectMany(m => m.Overrides).Contains(overridden) &&
                method.Parameters.Select(p => p.ParameterType).SequenceEqual(overridden.Parameters.Select(p => p.ParameterType));
        }
    }
}
