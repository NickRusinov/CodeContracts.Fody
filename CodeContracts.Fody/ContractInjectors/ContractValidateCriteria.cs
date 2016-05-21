using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractValidateCriteria : IContractValidateCriteria
    {
        public bool IsContractValidate(MethodDefinition methodDefinition)
        {
            return methodDefinition.IsStatic &&
                   methodDefinition.IsPublic &&
                   !methodDefinition.IsGetter &&
                   !methodDefinition.IsSetter &&
                   !methodDefinition.IsConstructor &&
                   !methodDefinition.HasGenericParameters &&
                   methodDefinition.Name.StartsWith("Validate") &&
                   Equals(methodDefinition.ReturnType.Resolve(), methodDefinition.Module.TypeSystem.Boolean.Resolve());
        }
    }
}
