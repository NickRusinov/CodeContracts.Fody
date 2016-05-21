using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractValidateScanner : IContractValidateScanner
    {
        private readonly IContractValidateCriteria contractValidateCriteria;

        public ContractValidateScanner(IContractValidateCriteria contractValidateCriteria)
        {
            Contract.Requires(contractValidateCriteria != null);

            this.contractValidateCriteria = contractValidateCriteria;
        }

        public IEnumerable<ContractValidateDefinition> Scan(CustomAttribute customAttribute)
        {
            var contractExceptionType = customAttribute.AttributeType.Module.ImportReference(typeof(ContractExceptionAttribute));
            var contractMessageType = customAttribute.AttributeType.Module.ImportReference(typeof(ContractMessageAttribute));
            var exceptionType = customAttribute.AttributeType.Module.ImportReference(typeof(Exception)).Resolve();
            
            return from methodDefinition in customAttribute.AttributeType.Resolve().GetMethods()
                   where contractValidateCriteria.IsContractValidate(methodDefinition)
                   let methodExceptionValue = ResolveAttributeValue<TypeReference>(methodDefinition, contractExceptionType)?.Resolve()
                   let classExceptionValue = ResolveAttributeValue<TypeReference>(customAttribute.AttributeType.Resolve(), contractExceptionType)?.Resolve()
                   let methodMessageValue = ResolveAttributeValue<string>(methodDefinition, contractMessageType)
                   let classMessageValue = ResolveAttributeValue<string>(customAttribute.AttributeType.Resolve(), contractMessageType)
                   select new ContractValidateDefinition(methodDefinition, methodExceptionValue ?? classExceptionValue ?? exceptionType, methodMessageValue ?? classMessageValue);
        }

        private static T ResolveAttributeValue<T>(ICustomAttributeProvider customAttributeProvider, TypeReference attributeTypeReference)
            where T : class
        {
            return customAttributeProvider.CustomAttributes
                .SingleOrDefault(ca => Equals(ca.AttributeType.Resolve(), attributeTypeReference.Resolve()))?.ConstructorArguments
                .Select(caa => (T)caa.Value)
                .SingleOrDefault();
        }
    }
}
