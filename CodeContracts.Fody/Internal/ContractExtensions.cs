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
    internal static class ContractExtensions
    {
        /// <summary>
        /// Resolves and imports <see cref="Contract.Ensures(bool)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference ImportEnsures(this ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportMethod("Ensures", 1, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Ensures(bool, string)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference ImportEnsuresWithMessage(this ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportMethod("Ensures", 2, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Invariant(bool)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference ImportInvariant(this ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportMethod("Invariant", 1, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Invariant(bool, string)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference ImportInvariantWithMessage(this ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportMethod("Invariant", 2, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Requires(bool)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference ImportRequires(this ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportMethod("Requires", 1, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Requires(bool, string)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved method</returns>
        public static MethodReference ImportRequiresWithMessage(this ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportMethod("Requires", 2, 0);
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Requires{TException}(bool)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <param name="genericReference">Type of exception for requires method</param>
        /// <returns>Resolved method</returns>
        public static MethodReference ImportRequiresWithException(this ModuleDefinition moduleDefinition, TypeReference genericReference)
        {
            return moduleDefinition.ImportMethod("Requires", 1, 1).MakeGeneric(moduleDefinition.ImportReference(genericReference));
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Requires{TException}(bool, string)"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <param name="genericReference">Type of exception for requires method</param>
        /// <returns>Resolved method</returns>
        public static MethodReference ImportRequiresWithExceptionAndMessage(this ModuleDefinition moduleDefinition, TypeReference genericReference)
        {
            return moduleDefinition.ImportMethod("Requires", 2, 1).MakeGeneric(moduleDefinition.ImportReference(genericReference));
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract.Result{T}()"/> method
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <param name="genericReference">Type of return value for method</param>
        /// <returns>Resolved method</returns>
        public static MethodReference ImportResult(this ModuleDefinition moduleDefinition, TypeReference genericReference)
        {
            return moduleDefinition.ImportMethod("Result", 0, 1).MakeGeneric(moduleDefinition.ImportReference(genericReference));
        }

        /// <summary>
        /// Resolves and imports <see cref="Contract"/> type
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved type</returns>
        public static TypeReference ImportContract(this ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportReference(typeof(Contract));
        }

        /// <summary>
        /// Resolves and imports <see cref="ContractClassAttribute"/> type
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved type</returns>
        public static TypeReference ImportContractClass(this ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportReference(typeof(ContractClassAttribute));
        }

        /// <summary>
        /// Resolves and imports <see cref="ContractClassForAttribute"/> type
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved type</returns>
        public static TypeReference ImportContractClassFor(this ModuleDefinition moduleDefinition)
        {
            return moduleDefinition.ImportReference(typeof(ContractClassForAttribute));
        }

        /// <summary>
        /// Resolves and imports <see cref="ContractInvariantMethodAttribute"/> type
        /// </summary>
        /// <param name="moduleDefinition">Definition of module for import reference</param>
        /// <returns>Resolved type</returns>
        public static TypeReference ImportContractInvariantMethod(this ModuleDefinition moduleDefinition)
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
        private static MethodReference ImportMethod(this ModuleDefinition moduleDefinition, string name, int parameters, int genericParameters)
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
