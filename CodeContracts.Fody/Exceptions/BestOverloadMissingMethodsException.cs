using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.Exceptions
{
    /// <summary>
    /// Represents exception for missing overload methods while resolves best oveload method
    /// </summary>
    [Serializable]
    public class BestOverloadMissingMethodsException : CodeContractsFodyException
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="BestOverloadMissingMethodsException"/>
        /// </summary>
        public BestOverloadMissingMethodsException()
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="BestOverloadMissingMethodsException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public BestOverloadMissingMethodsException(string message) 
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="BestOverloadMissingMethodsException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">The exception that is the cause of the current exception</param>
        public BestOverloadMissingMethodsException(string message, Exception inner) 
            : base(message, inner)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="BestOverloadMissingMethodsException"/>
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination</param>
        protected BestOverloadMissingMethodsException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="BestOverloadMissingMethodsException"/>
        /// </summary>
        /// <param name="allMethodReferences">Collection of all overload methods</param>
        public BestOverloadMissingMethodsException(IReadOnlyCollection<MethodReference> allMethodReferences)
            : base(FormatMessage(allMethodReferences), TraceLevel.Error)
        {
            AllMethodReferences = allMethodReferences;
        }

        /// <summary>
        /// Collection of all overload methods
        /// </summary>
        public IReadOnlyCollection<MethodReference> AllMethodReferences { get; }

        /// <summary>
        /// Formats error message for exception
        /// </summary>
        /// <param name="allMethodReferences">Collection of all overload methods</param>
        /// <returns>Formatted error message</returns>
        private static string FormatMessage(IReadOnlyCollection<MethodReference> allMethodReferences)
        {
            return
                "Can not find best overload method from methods:" + Environment.NewLine +
                string.Join(Environment.NewLine, allMethodReferences);
        }
    }
}
