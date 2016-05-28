using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody.Configurations
{
    public class LoggerConfiguration
    {
        public void Configure(ModuleWeaver moduleWeaver)
        {
            Logger.Current = new Logger(moduleWeaver.LogDebug, moduleWeaver.LogInfo, moduleWeaver.LogWarning, moduleWeaver.LogError);
        }
    }
}
