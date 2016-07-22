using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody
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
        public CodeContractsFodyException()
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="CodeContractsFodyException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public CodeContractsFodyException(string message) 
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of class <see cref="CodeContractsFodyException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">The exception that is the cause of the current exception</param>
        public CodeContractsFodyException(string message, Exception inner) 
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
    }
}
