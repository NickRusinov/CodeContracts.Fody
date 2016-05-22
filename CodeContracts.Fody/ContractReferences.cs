using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody
{
    public static class ContractReferences
    {
        public static MethodReference Ensures(ModuleDefinition moduleDefinition) => FindMethod(moduleDefinition, "Ensures", 1, 0);

        public static MethodReference EnsuresWithMessage(ModuleDefinition moduleDefinition) => FindMethod(moduleDefinition, "Ensures", 2, 0);

        public static MethodReference Invariant(ModuleDefinition moduleDefinition) => FindMethod(moduleDefinition, "Invariant", 1, 0);

        public static MethodReference InvariantWithMessage(ModuleDefinition moduleDefinition) => FindMethod(moduleDefinition, "Invariant", 2, 0);

        public static MethodReference Requires(ModuleDefinition moduleDefinition) => FindMethod(moduleDefinition, "Requires", 1, 0);

        public static MethodReference RequiresWithMessage(ModuleDefinition moduleDefinition) => FindMethod(moduleDefinition, "Requires", 2, 0);

        public static MethodReference RequiresWithException(ModuleDefinition moduleDefinition, TypeReference genericReference) => FindMethod(moduleDefinition, "Requires", 1, 1).MakeGeneric(genericReference);

        public static MethodReference RequiresWithExceptionAndMessage(ModuleDefinition moduleDefinition, TypeDefinition genericReference) => FindMethod(moduleDefinition, "Requires", 2, 1).MakeGeneric(genericReference);

        public static MethodReference Result(ModuleDefinition moduleDefinition, TypeDefinition genericReference) => FindMethod(moduleDefinition, "Result", 0, 1).MakeGeneric(genericReference);

        private static MethodReference FindMethod(ModuleDefinition moduleDefinition, string name, int parameters, int genericParameters)
        {
            return moduleDefinition.ImportReference(typeof(Contract)).Resolve().Methods
                .Where(md => md.Name == name)
                .Where(md => md.Parameters.Count == parameters)
                .Single(md => md.GenericParameters.Count == genericParameters);
        }
    }
}
