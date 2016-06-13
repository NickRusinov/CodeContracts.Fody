using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody
{
    internal sealed class Logger
    {
        public static Logger Instance { get; set; } = new Logger();

        private readonly Action<string> logDebug;

        private readonly Action<string> logInfo;

        private readonly Action<string> logWarning;

        private readonly Action<string> logError;

        public Logger(Action<string> logDebug = null, Action<string> logInfo = null, Action<string> logWarning = null, Action<string> logError = null)
        {
            this.logDebug = logDebug;
            this.logInfo = logInfo;
            this.logWarning = logWarning;
            this.logError = logError;
        }

        public void LogDebug(string message) => logDebug?.Invoke(message);

        public void LogInfo(string message) => logInfo?.Invoke(message);

        public void LogWarning(string message) => logWarning?.Invoke(message);

        public void LogError(string message) => logError?.Invoke(message);
    }
}
