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
    /// Represents exception for warnings that async methods currently not supported
    /// </summary>
    [Serializable]
    public class AsyncMethodsNotSupportedException : CodeContractsFodyException
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="AsyncMethodsNotSupportedException"/>
        /// </summary>
        public AsyncMethodsNotSupportedException()
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="AsyncMethodsNotSupportedException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public AsyncMethodsNotSupportedException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="AsyncMethodsNotSupportedException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">The exception that is the cause of the current exception</param>
        public AsyncMethodsNotSupportedException(string message, Exception inner)
            : base(message, inner)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="AsyncMethodsNotSupportedException"/>
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination</param>
        protected AsyncMethodsNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="AsyncMethodsNotSupportedException"/>
        /// </summary>
        /// <param name="methodDefinition">Definition of async method</param>
        public AsyncMethodsNotSupportedException(MethodDefinition methodDefinition)
            : base(FormatMessage(methodDefinition), TraceLevel.Error)
        {
            MethodDefinition = methodDefinition;
        }

        /// <summary>
        /// Definition of async method
        /// </summary>
        public MethodDefinition MethodDefinition { get; }

        /// <summary>
        /// Formats error message for exception
        /// </summary>
        /// <param name="methodDefinition">Definition of async method</param>
        /// <returns>Formatted error message</returns>
        private static string FormatMessage(MethodDefinition methodDefinition)
        {
            return
                "Async methods currently not supported:" + Environment.NewLine +
                methodDefinition;
        }
    }
}
