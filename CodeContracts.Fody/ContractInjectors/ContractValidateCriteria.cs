using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Criteria that defines that specified method is contract validate method
    /// </summary>
    public class ContractValidateCriteria : IContractValidateCriteria
    {
        /// <inheritdoc/>
        public bool IsContractValidate(MethodDefinition methodDefinition)
        {
            return methodDefinition.IsStatic &&
                   methodDefinition.IsPublic &&
                   !methodDefinition.IsGetter &&
                   !methodDefinition.IsSetter &&
                   !methodDefinition.IsConstructor &&
                   !methodDefinition.HasGenericParameters &&
                   methodDefinition.Name.StartsWith("Validate") &&
                   TypeReferenceComparer.Instance.Equals(methodDefinition.ReturnType, methodDefinition.Module.TypeSystem.Boolean);
        }
    }
}
