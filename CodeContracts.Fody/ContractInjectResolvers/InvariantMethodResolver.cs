using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectBuilders;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using static CodeContracts.Fody.Internal.ContractReferences;

namespace CodeContracts.Fody.ContractInjectResolvers
{
    /// <summary>
    /// Resolves method which is a invariant method for specified type
    /// </summary>
    public class InvariantMethodResolver : IInvariantMethodResolver
    {
        /// <summary>
        /// Definition of current weaving assembly
        /// </summary>
        private readonly ModuleDefinition moduleDefinition;

        /// <summary>
        /// Creates a new invariant method for specified type
        /// </summary>
        private readonly IInvariantMethodBuilder invariantMethodBuilder;

        /// <summary>
        /// Initializes a new instance of class <see cref="InvariantMethodResolver"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <param name="invariantMethodBuilder">Creates a new invariant method for specified type</param>
        public InvariantMethodResolver(ModuleDefinition moduleDefinition, IInvariantMethodBuilder invariantMethodBuilder)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(invariantMethodBuilder != null);

            this.moduleDefinition = moduleDefinition;
            this.invariantMethodBuilder = invariantMethodBuilder;
        }

        /// <inheritdoc/>
        public MethodDefinition Resolve(TypeDefinition typeDefinition)
        {
            var contractInvariantMethod = ResolveInvariantMethod(typeDefinition);

            if (contractInvariantMethod == null)
                return invariantMethodBuilder.Build(typeDefinition);

            return contractInvariantMethod;
        }

        /// <summary>
        /// Resovles a invariant method for specified type if its already exists
        /// </summary>
        /// <param name="typeDefinition">Type for which resolves a invariant method</param>
        /// <returns>Invariant method for specified type if its exists; null otherwise</returns>
        protected MethodDefinition ResolveInvariantMethod(TypeDefinition typeDefinition)
        {
            Contract.Requires(typeDefinition != null);

            return typeDefinition.Methods
                .SingleOrDefault(md => md.CustomAttributes
                    .Select(ca => ca.AttributeType)
                    .Contains(ContractInvariantMethod(moduleDefinition), TypeReferenceComparer.Instance));
        }
    }
}
