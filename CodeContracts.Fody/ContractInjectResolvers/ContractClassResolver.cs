using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectResolvers
{
    public class ContractClassResolver : IContractClassResolver
    {
        private readonly IInterfaceContractClassBuilder interfaceContractClassBuilder;

        private readonly IAbstractContractClassBuilder abstractContractClassBuilder;

        public ContractClassResolver(IInterfaceContractClassBuilder interfaceContractClassBuilder, IAbstractContractClassBuilder abstractContractClassBuilder)
        {
            Contract.Requires(interfaceContractClassBuilder != null);
            Contract.Requires(abstractContractClassBuilder != null);

            this.interfaceContractClassBuilder = interfaceContractClassBuilder;
            this.abstractContractClassBuilder = abstractContractClassBuilder;
        }
        
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

        protected TypeDefinition ResolveContractClass(TypeDefinition typeDefinition)
        {
            Contract.Requires(typeDefinition != null);

            var contractClassDefinition = typeDefinition.Module.ImportReference(typeof(ContractClassAttribute)).Resolve();
            var contractClassAttribute = typeDefinition.CustomAttributes.SingleOrDefault(ca => ca.AttributeType.Resolve() == contractClassDefinition);

            return (TypeDefinition)contractClassAttribute?.ConstructorArguments.Single().Value;
        }
    }
}
