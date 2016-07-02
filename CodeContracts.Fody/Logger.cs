using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody
{
    /// <summary>
    /// Simple logger for application
    /// </summary>
    internal sealed class Logger
    {
        /// <summary>
        /// Singleton logger for application
        /// </summary>
        public static Logger Instance { get; set; } = new Logger();

        /// <summary>
        /// Log debug level message
        /// </summary>
        private readonly Action<string> logDebug;

        /// <summary>
        /// Log info level message
        /// </summary>
        private readonly Action<string> logInfo;

        /// <summary>
        /// Log warning level message
        /// </summary>
        private readonly Action<string> logWarning;

        /// <summary>
        /// Log error level message
        /// </summary>
        private readonly Action<string> logError;

        /// <summary>
        /// Initializes a new instance of class <see cref="Logger"/>
        /// </summary>
        /// <param name="logDebug">Log debug level message. Can be null</param>
        /// <param name="logInfo">Log info level message. Can be null</param>
        /// <param name="logWarning">Log warning level message. Can be null</param>
        /// <param name="logError">Log error level message. Can be null</param>
        public Logger(Action<string> logDebug = null, Action<string> logInfo = null, Action<string> logWarning = null, Action<string> logError = null)
        {
            this.logDebug = logDebug;
            this.logInfo = logInfo;
            this.logWarning = logWarning;
            this.logError = logError;
        }

        /// <summary>
        /// Log debug level message
        /// </summary>
        /// <param name="message">Message that be logged</param>
        public void LogDebug(string message) => logDebug?.Invoke(message);

        /// <summary>
        /// Log info level message
        /// </summary>
        /// <param name="message">Message that be logged</param>
        public void LogInfo(string message) => logInfo?.Invoke(message);

        /// <summary>
        /// Log warning level message
        /// </summary>
        /// <param name="message">Message that be logged</param>
        public void LogWarning(string message) => logWarning?.Invoke(message);

        /// <summary>
        /// Log error level message
        /// </summary>
        /// <param name="message">Message that be logged</param>
        public void LogError(string message) => logError?.Invoke(message);
    }
}
