using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody.Exceptions
{
    /// <summary>
    /// Base class for all application exceptions of current Fody addin
    /// </summary>
    [Serializable]
    public abstract class CodeContractsFodyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="CodeContractsFodyException"/>
        /// </summary>
        protected CodeContractsFodyException()
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="CodeContractsFodyException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        protected CodeContractsFodyException(string message) 
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="CodeContractsFodyException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">The exception that is the cause of the current exception</param>
        protected CodeContractsFodyException(string message, Exception inner) 
            : base(message, inner)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="CodeContractsFodyException"/>
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination</param>
        protected CodeContractsFodyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="CodeContractsFodyException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="level">Exception level (info, warning or error)</param>
        protected CodeContractsFodyException(string message, TraceLevel level)
            : base(message)
        {
            Level = level;
        }

        /// <summary>
        /// Exception level (info, warning or error)
        /// </summary>
        public TraceLevel Level { get; } = TraceLevel.Info;
    }
}
