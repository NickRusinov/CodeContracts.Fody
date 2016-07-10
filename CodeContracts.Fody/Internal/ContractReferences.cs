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
    /// Conatins methods for resolves and imports <see cref="Contract"/>'s methods and some attributes
    /// </summary>
    internal static class ContractReferences
    {
        /// <summary>
        /// Resolves and imports <see cref="Contract.Ensures(bool)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference Ensures(ModuleDefinition moduleDefinition)
        {
            return FindMethod(moduleDefinition, "Ensures", 1, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Ensures(bool, string)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference EnsuresWithMessage(ModuleDefinition moduleDefinition)
        {
            return FindMethod(moduleDefinition, "Ensures", 2, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Invariant(bool)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference Invariant(ModuleDefinition moduleDefinition)
        {
            return FindMethod(moduleDefinition, "Invariant", 1, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Invariant(bool, string)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference InvariantWithMessage(ModuleDefinition moduleDefinition)
        {
            return FindMethod(moduleDefinition, "Invariant", 2, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Requires(bool)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference Requires(ModuleDefinition moduleDefinition)
        {
            return FindMethod(moduleDefinition, "Requires", 1, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Requires(bool, string)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference RequiresWithMessage(ModuleDefinition moduleDefinition)
        {
            return FindMethod(moduleDefinition, "Requires", 2, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Requires{TException}(bool)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <param name="genericReference">Type of exception for requires method</param>
        /// <returns>Resolved method</returns>
        public static MethodReference RequiresWithException(ModuleDefinition moduleDefinition, TypeReference genericReference)
        {
            return FindMethod(moduleDefinition, "Requires", 1, 1).MakeGeneric(genericReference);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Requires{TException}(bool, string)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <param name="genericReference">Type of exception for requires method</param>
        /// <returns>Resolved method</returns>
        public static MethodReference RequiresWithExceptionAndMessage(ModuleDefinition moduleDefinition, TypeReference genericReference)
        {
            return FindMethod(moduleDefinition, "Requires", 2, 1).MakeGeneric(genericReference);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Result{T}()"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <param name="genericReference">Type of return value for method</param>
        /// <returns>Resolved method</returns>
        public static MethodReference Result(ModuleDefinition moduleDefinition, TypeReference genericReference)
        {
            return FindMethod(moduleDefinition, "Result", 0, 1).MakeGeneric(genericReference);
        }

        /// <summary>
        /// Resolves and imports <see cref="ContractClassAttribute"/> type
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved type</returns>
        public static TypeReference ContractClass(ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportReference(typeof(ContractClassAttribute));
        }

        /// <summary>
        /// Resolves and imports <see cref="ContractClassForAttribute"/> type
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved type</returns>
        public static TypeReference ContractClassFor(ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportReference(typeof(ContractClassForAttribute));
        }

        /// <summary>
        /// Resolves and imports <see cref="ContractInvariantMethodAttribute"/> type
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved type</returns>
        public static TypeReference ContractInvariantMethod(ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportReference(typeof(ContractInvariantMethodAttribute));
        }

        /// <summary>
        /// Resolves and import <see cref="Contract"/>'s method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <param name="name">Name of method for resolves</param>
        /// <param name="parameters">Number of parameters of method for resolves</param>
        /// <param name="genericParameters">Number of generic parameters of method for resolves</param>
        /// <returns>Resolved method</returns>
        private static MethodReference FindMethod(ModuleDefinition moduleDefinition, string name, int parameters, int genericParameters)
        {
            return moduleDefinition.ImportReference(typeof(Contract)).Resolve().Methods
                .Where(md => md.Name == name)
                .Where(md => md.Parameters.Count == parameters)
                .Where(md => md.GenericParameters.Count == genericParameters)
                .Select(moduleDefinition.ImportReference)
                .Single();
        }
    }
}
