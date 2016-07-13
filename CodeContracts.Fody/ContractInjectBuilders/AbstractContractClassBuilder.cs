using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectBuilders
{
    /// <summary>
    /// Creates a new contract class for specified type which is abstract class
    /// </summary>
    public class AbstractContractClassBuilder : IAbstractContractClassBuilder
    {
        /// <inheritdoc/>
        public TypeDefinition Build(TypeDefinition typeDefinition)
        {
            var contractClass = BuildContractClassDefinition(typeDefinition);
            var contractClassAttribute = BuildContractClassAttribute(typeDefinition.Module, contractClass);
            var contractClassForAttribute = BuildContractClassForAttribute(typeDefinition.Module, typeDefinition);

            typeDefinition.CustomAttributes.Add(contractClassAttribute);
            contractClass.CustomAttributes.Add(contractClassForAttribute);
            typeDefinition.Module.Types.Add(contractClass);

            return contractClass;
        }

        /// <summary>
        /// Creates a new contract class for existing class
        /// </summary>
        /// <param name="typeDefinition">Type for which creates a new contract class</param>
        /// <returns>A new contract class for existing class</returns>
        private TypeDefinition BuildContractClassDefinition(TypeDefinition typeDefinition)
        {
            var newTypeDefinition = new TypeDefinition(typeDefinition.Namespace, $"{ typeDefinition.Name }Contracts<G>", TypeAttributes.NotPublic | TypeAttributes.Abstract, typeDefinition);
            newTypeDefinition.Methods.AddRange(typeDefinition.Methods.Where(md => md.IsVirtual).Select(BuildMethodDefinition));

            return newTypeDefinition;
        }

        /// <summary>
        /// Creates a new overriden method for contract class
        /// </summary>
        /// <param name="methodDefinition">Method for which creates a new overriden method</param>
        /// <returns>A new overriden method for contract class</returns>
        private MethodDefinition BuildMethodDefinition(MethodDefinition methodDefinition)
        {
            var newMethodDefinition = new MethodDefinition(methodDefinition.Name, methodDefinition.Attributes & ~MethodAttributes.Abstract & ~MethodAttributes.NewSlot, methodDefinition.ReturnType);
            newMethodDefinition.Overrides.Add(methodDefinition);
            newMethodDefinition.Parameters.AddRange(methodDefinition.Parameters.Select(BuildParameterDefinition));
            newMethodDefinition.GenericParameters.AddRange(methodDefinition.GenericParameters.Select(BuildGenericDefinition));
            
            return newMethodDefinition;
        }

        /// <summary>
        /// Creates copy of a existing <see cref="ParameterDefinition"/> of a method
        /// </summary>
        /// <param name="parameterDefinition">Parameter definition</param>
        /// <returns>Copy of a existing <see cref="ParameterDefinition"/> of a method</returns>
        private ParameterDefinition BuildParameterDefinition(ParameterDefinition parameterDefinition)
        {
            return new ParameterDefinition(parameterDefinition.Name, parameterDefinition.Attributes, parameterDefinition.ParameterType);
        }

        /// <summary>
        /// Creates copy of a existing <see cref="GenericParameter"/> of a method
        /// </summary>
        /// <param name="genericParameterDefinition">Generic parameter</param>
        /// <returns>Copy of a existing <see cref="GenericParameter"/> of a method</returns>
        private GenericParameter BuildGenericDefinition(GenericParameter genericParameterDefinition)
        {
            return new GenericParameter(genericParameterDefinition.Name, genericParameterDefinition.Owner);
        }

        /// <summary>
        /// Creates a new custom attribute <see cref="ContractClassAttribute"/> for type for which creates contract class
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <param name="typeDefinition">Type which is a contract class</param>
        /// <returns>A new custom attribute <see cref="ContractClassAttribute"/> for type for which creates contract class</returns>
        private CustomAttribute BuildContractClassAttribute(ModuleDefinition moduleDefinition, TypeDefinition typeDefinition)
        {
            var customAttribute = new CustomAttribute(moduleDefinition.ImportReference(typeof(ContractClassAttribute).GetConstructor(new[] { typeof(Type) })));
            customAttribute.ConstructorArguments.Add(new CustomAttributeArgument(moduleDefinition.ImportReference(typeof(Type)), typeDefinition));

            return customAttribute;
        }

        /// <summary>
        /// Creates a new custom attribute <see cref="ContractClassForAttribute"/> for contract class
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <param name="typeDefinition">Type for which creates a contract class</param>
        /// <returns>A new custom attribute <see cref="ContractClassForAttribute"/> for contract class</returns>
        private CustomAttribute BuildContractClassForAttribute(ModuleDefinition moduleDefinition, TypeDefinition typeDefinition)
        {
            var customAttribute = new CustomAttribute(moduleDefinition.ImportReference(typeof(ContractClassForAttribute).GetConstructor(new[] { typeof(Type) })));
            customAttribute.ConstructorArguments.Add(new CustomAttributeArgument(moduleDefinition.ImportReference(typeof(Type)), typeDefinition));

            return customAttribute;
        }
    }
}
