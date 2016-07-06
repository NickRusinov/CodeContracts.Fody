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
    /// <summary>
    /// Contains extension methods for <see cref="MethodReference"/> and <see cref="MethodDefinition"/>
    /// </summary>
    internal static class MethodReferenceExtensions
    {
        /// <summary>
        /// Method that answer does specified <paramref name="method"/> overrides <paramref name="overridden"/>
        /// </summary>
        /// <param name="method">Method that overrides another method</param>
        /// <param name="overridden">Method that overriden by another method</param>
        /// <returns>Does method <paramref name="method"/> overrides method <paramref name="overridden"/></returns>
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

        /// <summary>
        /// Creates new closed generic method that maked from open generic method with specified types
        /// </summary>
        /// <param name="methodReference">Open generic method</param>
        /// <param name="genericTypeReferences">Types for closes open generic method</param>
        /// <returns>Closed generic method</returns>
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
