using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.Internal
{
    internal static class TypeReferenceExtensions
    {
        public static IEnumerable<TypeReference> GetBaseTypes(this TypeDefinition typeDefinition)
        {
            Contract.Requires(typeDefinition != null);
            Contract.Ensures(Contract.Result<IEnumerable<TypeReference>>() != null);

            if (typeDefinition.BaseType == null)
                return Enumerable.Empty<TypeReference>();

            return Enumerable.Repeat(typeDefinition.BaseType, 1).Concat(typeDefinition.BaseType.Resolve().GetBaseTypes());
        }

        public static TypeReference MakeGeneric(this TypeReference typeReference, params TypeReference[] genericTypeRefrerences)
        {
            Contract.Requires(typeReference != null);
            Contract.Requires(genericTypeRefrerences != null);
            Contract.Ensures(Contract.Result<TypeReference>() != null);

            var genericTypeReference = new GenericInstanceType(typeReference);
            genericTypeReference.GenericArguments.AddRange(genericTypeRefrerences);

            return genericTypeReference;
        }
    }
}
