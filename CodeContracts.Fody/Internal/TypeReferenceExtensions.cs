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
    /// Contains exetension method for <see cref="TypeReference"/> and <see cref="TypeDefinition"/>
    /// </summary>
    internal static class TypeReferenceExtensions
    {
        /// <summary>
        /// Get all base types for specified type exclude itself
        /// </summary>
        /// <param name="typeReference">Type for that gets base types</param>
        /// <returns>All base types for <paramref name="typeReference"/></returns>
        public static IEnumerable<TypeReference> GetBaseTypes(this TypeReference typeReference)
        {
            Contract.Requires(typeReference != null);
            Contract.Ensures(Contract.Result<IEnumerable<TypeReference>>() != null);

            var typeDefinition = typeReference.Resolve();
            if (typeDefinition.BaseType == null)
                return Enumerable.Empty<TypeReference>();

            return Enumerable.Repeat(typeDefinition.BaseType, 1).Concat(typeDefinition.BaseType.GetBaseTypes());
        }

        /// <summary>
        /// Get all interface for specified type exclude itself
        /// </summary>
        /// <param name="typeReference">Type for that gets interfaces</param>
        /// <returns>All interfaces for <paramref name="typeReference"/></returns>
        public static IEnumerable<TypeReference> GetInterfaces(this TypeReference typeReference)
        {
            Contract.Requires(typeReference != null);
            Contract.Ensures(Contract.Result<IEnumerable<TypeReference>>() != null);

            var typeDefinition = typeReference.Resolve();
            if (typeDefinition.BaseType == null)
                return Enumerable.Empty<TypeReference>();

            return typeDefinition.Interfaces.Concat(typeDefinition.BaseType.GetInterfaces());
        }

        /// <summary>
        /// Resolves all primitive numeric types to thats can implicit converts specified numeric type
        /// </summary>
        /// <param name="typeReference">Specified numeric type</param>
        /// <returns>All numeric types to that can implicit converts <paramref name="typeReference"/></returns>
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

        /// <summary>
        /// Is specified type signed number (<see cref="sbyte"/>, <see cref="short"/>, <see cref="int"/> or 
        /// <see cref="long"/>)
        /// </summary>
        /// <param name="typeReference">Specified type</param>
        /// <returns>Is <paramref name="typeReference"/> signed number</returns>
        public static bool IsSignedNumeric(this TypeReference typeReference)
        {
            Contract.Requires(typeReference != null);

            var typeSystem = typeReference.Module.TypeSystem;

            return TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.SByte) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.Int16) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.Int32) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.Int64);
        }

        /// <summary>
        /// Is specified type unsigned numeric (<see cref="byte"/>, <see cref="char"/>, <see cref="ushort"/>, 
        /// <see cref="uint"/> or <see cref="uint"/>)
        /// </summary>
        /// <param name="typeReference">Specified type</param>
        /// <returns>Is <paramref name="typeReference"/> unsigned number</returns>
        public static bool IsUnsignedNumeric(this TypeReference typeReference)
        {
            Contract.Requires(typeReference != null);

            var typeSystem = typeReference.Module.TypeSystem;

            return TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.Byte) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.Char) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.UInt16) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.UInt32) ||
                   TypeReferenceComparer.Instance.Equals(typeReference, typeSystem.UInt64);
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

        /// <summary>
        /// Creates new closed generic type that maked from open generic type with specified types
        /// </summary>
        /// <param name="typeReference">Open generic type</param>
        /// <param name="genericTypeRefrerences">Types for closes open generic type</param>
        /// <returns>Closed generic type</returns>
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
