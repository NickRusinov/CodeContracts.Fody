using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractDefinitions
{
    /// <summary>
    /// Represents method of contract attribute that can be used in contract expression
    /// </summary>
    public class ContractValidateDefinition
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="ContractValidateDefinition"/>
        /// </summary>
        /// <param name="validateMethod">Method of contract attribute that can be used in contract expression</param>
        /// <param name="exceptionType">Exception type that will be raised when requires will fail</param>
        /// <param name="errorMessage">Error message that will be shown when contract will fail</param>
        public ContractValidateDefinition(MethodDefinition validateMethod, TypeDefinition exceptionType, string errorMessage)
        {
            Contract.Requires(validateMethod != null);
            
            ValidateMethod = validateMethod;
            ExceptionType = exceptionType;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Method of contract attribute that can be used in contract expression
        /// </summary>
        public MethodDefinition ValidateMethod { get; }

        /// <summary>
        /// Exception type that will be raised when requires will fail
        /// </summary>
        public TypeDefinition ExceptionType { get; }

        /// <summary>
        /// Error message that will be shown when contract will fail
        /// </summary>
        public string ErrorMessage { get; }
    }
}
