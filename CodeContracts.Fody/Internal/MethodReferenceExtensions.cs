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
    internal static class MethodReferenceExtensions
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

        public static MethodReference MakeGeneric(this MethodReference methodReference, params TypeReference[] genericTypeReferences)
        {
            Contract.Requires(methodReference != null);
            Contract.Requires(genericTypeReferences != null);
            Contract.Ensures(Contract.Result<MethodReference>() != null);

            var genericMethodReference = new MethodReference(methodReference.Name, methodReference.ReturnType, methodReference.DeclaringType);
            genericMethodReference.Parameters.AddRange(methodReference.Parameters.Select(pd => new ParameterDefinition(pd.ParameterType)));
            genericMethodReference.GenericParameters.AddRange(methodReference.GenericParameters.Select(gp => new GenericParameter(gp.Name, genericMethodReference)));
            
            return genericMethodReference;
        }
    }
}
