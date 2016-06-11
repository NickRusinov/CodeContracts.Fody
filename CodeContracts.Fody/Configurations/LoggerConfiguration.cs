using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody.Configurations
{
    /// <summary>
    /// Initializes configuration of logger
    /// </summary>
    public class LoggerConfiguration
    {
        /// <summary>
        /// Initializes configuration of logger
        /// </summary>
        /// <param name="moduleWeaver">ModuleWeaver instance for Fody addin</param>
        public void Configure(ModuleWeaver moduleWeaver)
        {
            Logger.Current = new Logger(moduleWeaver.LogDebug, moduleWeaver.LogInfo, moduleWeaver.LogWarning, moduleWeaver.LogError);
        }
    }
}
