using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Resolves a single parameter that representes as this reference for non static methods 
    /// and null reference for static methods
    /// </summary>
    public class ContractSelfResolver : IContractValidateParametersResolver
    {
        /// <inheritdoc/>
        public IEnumerable<ContractValidateParameter> Resolve(ContractDefinition contractDefinition, MethodDefinition methodDefinition)
        {
            if (methodDefinition.IsStatic || methodDefinition.IsConstructor)
                return new ContractValidateParameter(new ParameterDefinition("self", ParameterAttributes.Optional, methodDefinition.Module.TypeSystem.Void), new NullParameterBuilder());

            return new ContractValidateParameter(new ParameterDefinition("self", ParameterAttributes.Optional, contractDefinition.DeclaringType), new ThisParameterBuilder());
        }
    }
}
