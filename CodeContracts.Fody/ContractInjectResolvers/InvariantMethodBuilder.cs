using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectResolvers
{
    /// <summary>
    /// Creates a new invariant method for specified type
    /// </summary>
    public class InvariantMethodBuilder : IInvariantMethodBuilder
    {
        /// <inheritdoc/>
        public MethodDefinition Build(TypeDefinition typeDefinition)
        {
            var invariantMethod = BuildContractInvariantMethodDefinition(typeDefinition.Module);
            var invariantAttribute = BuildContractInvariantMethodAttribute(typeDefinition.Module);

            invariantMethod.CustomAttributes.Add(invariantAttribute);
            typeDefinition.Methods.Add(invariantMethod);

            return invariantMethod;
        }

        /// <summary>
        /// Creates a new invariant method for existing class
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <returns>A new invariant method for existing class</returns>
        private MethodDefinition BuildContractInvariantMethodDefinition(ModuleDefinition moduleDefinition)
        {
            return new MethodDefinition("Invariant<G>", MethodAttributes.Private, moduleDefinition.TypeSystem.Void);
        }

        /// <summary>
        /// Creates a new custom attribute <see cref="ContractInvariantMethodAttribute"/> for invariant method
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <returns>A new custom attribute <see cref="ContractInvariantMethodAttribute"/> for invariant method</returns>
        private CustomAttribute BuildContractInvariantMethodAttribute(ModuleDefinition moduleDefinition)
        {
            return new CustomAttribute(moduleDefinition.ImportReference(typeof(ContractInvariantMethodAttribute).GetConstructor(Type.EmptyTypes)));
        }
    }
}
