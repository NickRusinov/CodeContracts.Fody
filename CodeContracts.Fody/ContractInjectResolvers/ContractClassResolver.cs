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
    /// Resolves type which is a contract class for specified type
    /// </summary>
    public class ContractClassResolver : IContractClassResolver
    {
        /// <summary>
        /// Definition of current weaving assembly
        /// </summary>
        private readonly ModuleDefinition moduleDefinition;

        /// <summary>
        /// Creates a new contract class for specified type which is interface
        /// </summary>
        private readonly IInterfaceContractClassBuilder interfaceContractClassBuilder;

        /// <summary>
        /// Creates a new contract class for specified type which is abstract class
        /// </summary>
        private readonly IAbstractContractClassBuilder abstractContractClassBuilder;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractClassResolver"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <param name="interfaceContractClassBuilder">Creates a new contract class for specified type which is interface</param>
        /// <param name="abstractContractClassBuilder">Creates a new contract class for specified type which is abstract class</param>
        public ContractClassResolver(ModuleDefinition moduleDefinition, IInterfaceContractClassBuilder interfaceContractClassBuilder, IAbstractContractClassBuilder abstractContractClassBuilder)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(interfaceContractClassBuilder != null);
            Contract.Requires(abstractContractClassBuilder != null);

            this.moduleDefinition = moduleDefinition;
            this.interfaceContractClassBuilder = interfaceContractClassBuilder;
            this.abstractContractClassBuilder = abstractContractClassBuilder;
        }
        
        /// <inheritdoc/>
        public TypeDefinition Resolve(TypeDefinition typeDefinition, MethodDefinition methodDefinition)
        {
            var contractClass = ResolveContractClass(typeDefinition);

            if (typeDefinition.IsInterface && contractClass == null)
                return interfaceContractClassBuilder.Build(typeDefinition);

            if (typeDefinition.IsInterface && contractClass != null)
                return contractClass;

            if (typeDefinition.IsAbstract && methodDefinition?.IsAbstract == true && contractClass == null)
                return abstractContractClassBuilder.Build(typeDefinition);

            if (typeDefinition.IsAbstract && methodDefinition?.IsAbstract == true && contractClass != null)
                return contractClass;

            return typeDefinition;
        }

        /// <summary>
        /// Resolves a contract class for specified type if its already exists
        /// </summary>
        /// <param name="typeDefinition">Type for which resolves a contract class</param>
        /// <returns>Contract class for specified type if its exists; null otherwise</returns>
        private TypeDefinition ResolveContractClass(TypeDefinition typeDefinition)
        {
            Contract.Requires(typeDefinition != null);
            
            return (TypeDefinition)typeDefinition.CustomAttributes
                .SingleOrDefault(ca => TypeReferenceComparer.Instance.Equals(ca.AttributeType, ContractClass(moduleDefinition)))?.ConstructorArguments
                .Select(ca => ca.Value)
                .Single();
        }
    }
}
