using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.MethodBodyResolvers
{
    public class ContractClassResolver : IContractClassResolver
    {
        private readonly IContractClassResolver interfaceContractClassResolver;

        private readonly IContractClassResolver abstractContractClassResolver;

        public ContractClassResolver(IContractClassResolver interfaceContractClassResolver, IContractClassResolver abstractContractClassResolver)
        {
            Contract.Requires(interfaceContractClassResolver != null);
            Contract.Requires(abstractContractClassResolver != null);

            this.interfaceContractClassResolver = interfaceContractClassResolver;
            this.abstractContractClassResolver = abstractContractClassResolver;
        }
        
        public TypeDefinition Resolve(TypeDefinition typeDefinition, MethodDefinition methodDefinition)
        {
            var contractClass = ResolveContractClass(typeDefinition);

            if (typeDefinition.IsInterface && contractClass == null)
                return interfaceContractClassResolver.Resolve(typeDefinition, methodDefinition);

            if (typeDefinition.IsInterface && contractClass != null)
                return contractClass;

            if (typeDefinition.IsAbstract && methodDefinition?.IsAbstract == true && contractClass == null)
                return abstractContractClassResolver.Resolve(typeDefinition, methodDefinition);

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
