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
    /// Represents exception for ambiguous overload methods while resolves best oveload method
    /// </summary>
    [Serializable]
    public class BestOverloadAmbiguousMethodsException : CodeContractsFodyException
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="BestOverloadAmbiguousMethodsException"/>
        /// </summary>
        public BestOverloadAmbiguousMethodsException()
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="BestOverloadAmbiguousMethodsException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public BestOverloadAmbiguousMethodsException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="BestOverloadAmbiguousMethodsException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">The exception that is the cause of the current exception</param>
        public BestOverloadAmbiguousMethodsException(string message, Exception inner)
            : base(message, inner)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="BestOverloadAmbiguousMethodsException"/>
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination</param>
        protected BestOverloadAmbiguousMethodsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="BestOverloadAmbiguousMethodsException"/>
        /// </summary>
        /// <param name="allMethodReferences">Collection of all overload methods</param>
        /// <param name="ambiguousMethodReferences">Collection of ambiguous overload methods </param>
        public BestOverloadAmbiguousMethodsException(IReadOnlyCollection<MethodReference> allMethodReferences, IReadOnlyCollection<MethodReference> ambiguousMethodReferences)
            : base(FormatMessage(ambiguousMethodReferences), TraceLevel.Error)
        {
            AllMethodReferences = allMethodReferences;
            AmbiguousMethodReferences = ambiguousMethodReferences;
        }

        /// <summary>
        /// Collection of all overload methods
        /// </summary>
        public IReadOnlyCollection<MethodReference> AllMethodReferences { get; }

        /// <summary>
        /// Collection of ambiguous overload methods 
        /// </summary>
        public IReadOnlyCollection<MethodReference> AmbiguousMethodReferences { get; }
        
        /// <summary>
        /// Formats error message for exception
        /// </summary>
        /// <param name="ambiguousMethodReferences">Collection of ambiguous overload methods</param>
        /// <returns>Formatted error message</returns>
        private static string FormatMessage(IReadOnlyCollection<MethodReference> ambiguousMethodReferences)
        {
            return 
                "Ambiguous select best overload method from methods:" + Environment.NewLine +
                string.Join(Environment.NewLine, ambiguousMethodReferences);
        }
    }
}
