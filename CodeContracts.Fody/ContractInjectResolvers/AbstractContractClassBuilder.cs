using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectResolvers
{
    public class AbstractContractClassBuilder : IAbstractContractClassBuilder
    {
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

        private TypeDefinition BuildContractClassDefinition(TypeDefinition typeDefinition)
        {
            var newTypeDefinition = new TypeDefinition(typeDefinition.Namespace, $"{ typeDefinition.Name }Contracts{ Guid.NewGuid().ToString("N") }", TypeAttributes.NotPublic | TypeAttributes.Abstract, typeDefinition);
            newTypeDefinition.Methods.AddRange(typeDefinition.Methods.Where(md => md.IsVirtual).Select(BuildMethodDefinition));

            return newTypeDefinition;
        }

        private MethodDefinition BuildMethodDefinition(MethodDefinition methodDefinition)
        {
            var newMethodDefinition = new MethodDefinition(methodDefinition.Name, methodDefinition.Attributes & ~MethodAttributes.Abstract & ~MethodAttributes.NewSlot, methodDefinition.ReturnType);
            newMethodDefinition.Overrides.Add(methodDefinition);
            newMethodDefinition.Parameters.AddRange(methodDefinition.Parameters.Select(BuildParameterDefinition));
            newMethodDefinition.GenericParameters.AddRange(methodDefinition.GenericParameters.Select(BuildGenericDefinition));
            
            return newMethodDefinition;
        }

        private ParameterDefinition BuildParameterDefinition(ParameterDefinition parameterDefinition)
        {
            return new ParameterDefinition(parameterDefinition.Name, parameterDefinition.Attributes, parameterDefinition.ParameterType);
        }

        private GenericParameter BuildGenericDefinition(GenericParameter genericParameterDefinition)
        {
            return new GenericParameter(genericParameterDefinition.Name, genericParameterDefinition.Owner);
        }

        private CustomAttribute BuildContractClassAttribute(ModuleDefinition moduleDefinition, TypeDefinition typeDefinition)
        {
            var customAttribute = new CustomAttribute(moduleDefinition.ImportReference(typeof(ContractClassAttribute).GetConstructor(new[] { typeof(Type) })));
            customAttribute.ConstructorArguments.Add(new CustomAttributeArgument(moduleDefinition.ImportReference(typeof(Type)), typeDefinition));

            return customAttribute;
        }

        private CustomAttribute BuildContractClassForAttribute(ModuleDefinition moduleDefinition, TypeDefinition typeDefinition)
        {
            var customAttribute = new CustomAttribute(moduleDefinition.ImportReference(typeof(ContractClassForAttribute).GetConstructor(new[] { typeof(Type) })));
            customAttribute.ConstructorArguments.Add(new CustomAttributeArgument(moduleDefinition.ImportReference(typeof(Type)), typeDefinition));

            return customAttribute;
        }
    }
}
