using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.MethodBodyResolvers
{
    public class InvariantMethodBuilder : IInvariantMethodBuilder
    {
        public MethodDefinition Build(TypeDefinition typeDefinition)
        {
            var invariantMethod = BuildContractInvariantMethodDefinition(typeDefinition.Module);
            var invariantAttribute = BuildContractInvariantMethodAttribute(typeDefinition.Module);

            invariantMethod.CustomAttributes.Add(invariantAttribute);
            typeDefinition.Methods.Add(invariantMethod);

            return invariantMethod;
        }

        private MethodDefinition BuildContractInvariantMethodDefinition(ModuleDefinition moduleDefinition)
        {
            return new MethodDefinition($"Invariant{ Guid.NewGuid().ToString("N") }", MethodAttributes.Private, moduleDefinition.TypeSystem.Void);
        }

        private CustomAttribute BuildContractInvariantMethodAttribute(ModuleDefinition moduleDefinition)
        {
            return new CustomAttribute(moduleDefinition.ImportReference(typeof(ContractInvariantMethodAttribute).GetConstructor(Type.EmptyTypes)));
        }
    }
}
