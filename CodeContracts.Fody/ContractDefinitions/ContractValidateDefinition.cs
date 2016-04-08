using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractDefinitions
{
    public class ContractValidateDefinition
    {
        public ContractValidateDefinition(MethodDefinition validateMethod, TypeDefinition exceptionType, string errorMessage)
        {
            Contract.Requires(validateMethod != null);
            
            ValidateMethod = validateMethod;
            ExceptionType = exceptionType;
            ErrorMessage = errorMessage;
        }

        public MethodDefinition ValidateMethod { get; }

        public TypeDefinition ExceptionType { get; }

        public string ErrorMessage { get; }
    }
}
