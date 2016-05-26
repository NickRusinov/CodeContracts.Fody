using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Configurations;
using Mono.Cecil;

namespace CodeContracts.Fody
{
    public class ModuleWeaver
    {
        private readonly TinyIoCConfiguration tinyIoCConfiguration = new TinyIoCConfiguration();

        public ModuleDefinition ModuleDefinition { get; set; }
        
        public Action<string> LogDebug { get; set; }

        public Action<string> LogInfo { get; set; }

        public Action<string> LogWarning { get; set; }

        public void Execute()
        {
            Contract.Requires(ModuleDefinition != null);
            Contract.Requires(LogDebug != null);
            Contract.Requires(LogInfo != null);
            Contract.Requires(LogWarning != null);

            tinyIoCConfiguration.Configure(this);
        }
    }
}
