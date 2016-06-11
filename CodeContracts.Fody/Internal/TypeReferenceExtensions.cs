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
        public static IEnumerable<TypeReference> GetBaseTypes(this TypeReference typeReference)
        {
            Contract.Requires(typeReference != null);
            Contract.Ensures(Contract.Result<IEnumerable<TypeReference>>() != null);

            var typeDefinition = typeReference.Resolve();
            if (typeDefinition.BaseType == null)
                return Enumerable.Empty<TypeReference>();

            return Enumerable.Repeat(typeDefinition.BaseType, 1).Concat(typeDefinition.BaseType.GetBaseTypes());
        }

        public static IEnumerable<TypeReference> GetInterfaces(this TypeReference typeReference)
        {
            Contract.Requires(typeReference != null);
            Contract.Ensures(Contract.Result<IEnumerable<TypeReference>>() != null);

            var typeDefinition = typeReference.Resolve();
            if (typeDefinition.BaseType == null)
                return Enumerable.Empty<TypeReference>();

            return typeDefinition.Interfaces.Concat(typeDefinition.BaseType.GetInterfaces());
        }

        public static IEnumerable<TypeReference> GetImplicitNumericConversions(this TypeReference typeReference)
        {
            Contract.Requires(typeReference != null);
            Contract.Ensures(Contract.Result<IEnumerable<TypeReference>>() != null);

            var typeSystem = typeReference.Module.TypeSystem;

            if (TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.SByte))
                return new[] { typeSystem.Int16, typeSystem.Int32, typeSystem.Int64, typeSystem.Single, typeSystem.Double };

            if (TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.Byte))
                return new[] { typeSystem.Int16, typeSystem.UInt16, typeSystem.Int32, typeSystem.UInt32, typeSystem.Int64, typeSystem.UInt64, typeSystem.Single, typeSystem.Double };

            if (TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.Int16))
                return new[] { typeSystem.Int32, typeSystem.Int64, typeSystem.Single, typeSystem.Double };

            if (TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.UInt16))
                return new[] { typeSystem.Int32, typeSystem.UInt32, typeSystem.Int64, typeSystem.UInt64, typeSystem.Single, typeSystem.Double };

            if (TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.Char))
                return new[] { typeSystem.UInt16, typeSystem.Int32, typeSystem.UInt32, typeSystem.Int64, typeSystem.UInt64, typeSystem.Single, typeSystem.Double };

            if (TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.Int32))
                return new[] { typeSystem.Int64, typeSystem.Single, typeSystem.Double };

            if (TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.UInt32))
                return new[] { typeSystem.Int64, typeSystem.UInt64, typeSystem.Single, typeSystem.Double };

            if (TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.Int64))
                return new[] { typeSystem.Single, typeSystem.Double };

            if (TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.UInt64))
                return new[] { typeSystem.Single, typeSystem.Double };

            if (TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.Single))
                return new[] { typeSystem.Double };

            return Enumerable.Empty<TypeReference>();
        }

        public static bool IsSignedNumeric(this TypeReference typeReference)
        {
            Contract.Requires(typeReference != null);

            return TypeReferenceComparer.Instance.Equals(typeReference, typeReference.Module.TypeSystem.SByte) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeReference.Module.TypeSystem.Int16) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeReference.Module.TypeSystem.Int32) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeReference.Module.TypeSystem.Int64);
        }

        public static bool IsUnsignedNumeric(this TypeReference typeReference)
        {
            Contract.Requires(typeReference != null);

            return TypeReferenceComparer.Instance.Equals(typeReference, typeReference.Module.TypeSystem.Byte) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeReference.Module.TypeSystem.Char) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeReference.Module.TypeSystem.UInt16) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeReference.Module.TypeSystem.UInt32) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeReference.Module.TypeSystem.UInt64);
        }
        
        public static bool IsAssignable(this TypeReference typeReference, TypeReference toTypeReference)
        {
            Contract.Requires(typeReference != null);
            Contract.Requires(toTypeReference != null);

            return TypeReferenceComparer.Instance.Equals(typeReference, toTypeReference) ||
                   typeReference.GetImplicitNumericConversions().Contains(toTypeReference, TypeReferenceComparer.Instance) ||
                   typeReference.GetBaseTypes().Contains(toTypeReference, TypeReferenceComparer.Instance) ||
                   typeReference.GetInterfaces().Contains(toTypeReference, TypeReferenceComparer.Instance);
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
