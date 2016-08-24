using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace CodeContracts.Fody.ContractValidateResolvers
{
    /// <summary>
    /// Scans validate methods in a custom attribute
    /// </summary>
    public class ContractValidateScanner : IContractValidateScanner
    {
        /// <summary>
        /// Definition of current weaving assembly
        /// </summary>
        private readonly ModuleDefinition moduleDefinition;

        /// <summary>
        /// Criteria that defines that specified method is contract validate method
        /// </summary>
        private readonly IContractValidateCriteria contractValidateCriteria;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractValidateScanner"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <param name="contractValidateCriteria">Criteria that defines that specified method is contract validate method</param>
        public ContractValidateScanner(ModuleDefinition moduleDefinition, IContractValidateCriteria contractValidateCriteria)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(contractValidateCriteria != null);

            this.moduleDefinition = moduleDefinition;
            this.contractValidateCriteria = contractValidateCriteria;
        }

        /// <inheritdoc/>
        public IEnumerable<ContractValidateDefinition> Scan(CustomAttribute customAttribute)
        {
            var exceptionType = moduleDefinition.ImportReference(typeof(Exception)).Resolve();
            
            return from methodDefinition in customAttribute.AttributeType.Resolve().GetMethods()
                   where contractValidateCriteria.IsContractValidate(methodDefinition)
                   let methodExceptionValue = ResolveAttributeValue<TypeReference>(methodDefinition, "ContractExceptionAttribute")?.Resolve()
                   let classExceptionValue = ResolveAttributeValue<TypeReference>(customAttribute.AttributeType.Resolve(), "ContractExceptionAttribute")?.Resolve()
                   let methodMessageValue = ResolveAttributeValue<string>(methodDefinition, "ContractMessageAttribute")
                   let classMessageValue = ResolveAttributeValue<string>(customAttribute.AttributeType.Resolve(), "ContractMessageAttribute")
                   select new ContractValidateDefinition(methodDefinition, methodExceptionValue ?? classExceptionValue ?? exceptionType, methodMessageValue ?? classMessageValue);
        }

        /// <summary>
        /// Resolves single constructor's parameter of custom attribute of type name <paramref name="attributeTypeName"/> 
        /// that applied to <paramref name="customAttributeProvider"/>
        /// </summary>
        /// <typeparam name="T">Type of parameter</typeparam>
        /// <param name="customAttributeProvider">Member to which applied custom attributes</param>
        /// <param name="attributeTypeName">Name of type of custom attribute</param>
        /// <returns>Single constructor's parameter</returns>
        private static T ResolveAttributeValue<T>(ICustomAttributeProvider customAttributeProvider, string attributeTypeName)
            where T : class
        {
            return customAttributeProvider.CustomAttributes
                .SingleOrDefault(ca => ca.AttributeType.Name == attributeTypeName)?.ConstructorArguments
                .Select(caa => (T)caa.Value)
                .SingleOrDefault();
        }
    }
}
