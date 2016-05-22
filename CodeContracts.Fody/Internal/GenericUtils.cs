using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.Internal
{
    internal static class GenericUtils
    {
        public static TypeReference MakeGeneric(this TypeReference typeReference, params TypeReference[] genericTypes)
        {
            var genericTypeReference = new GenericInstanceType(typeReference);

            foreach (var genericParameter in genericTypes)
                genericTypeReference.GenericArguments.Add(genericParameter);

            return genericTypeReference;
        }

        public static MethodReference MakeGeneric(this MethodReference methodReference, params TypeReference[] genericTypes)
        {
            var genericMethodReference = new MethodReference(methodReference.Name, methodReference.ReturnType, methodReference.DeclaringType)
            {
                HasThis = methodReference.HasThis,
                ExplicitThis = methodReference.ExplicitThis,
                CallingConvention = methodReference.CallingConvention,
            };

            foreach (var parameter in methodReference.Parameters)
                genericMethodReference.Parameters.Add(new ParameterDefinition(parameter.ParameterType));

            foreach (var genericParameter in methodReference.GenericParameters)
                genericMethodReference.GenericParameters.Add(new GenericParameter(genericParameter.Name, genericMethodReference));

            return genericMethodReference;
        }
    }
}
