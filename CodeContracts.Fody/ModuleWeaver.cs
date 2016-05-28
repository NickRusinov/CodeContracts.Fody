using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CodeContracts.Fody.Configurations;
using Mono.Cecil;
using TinyIoC;

namespace CodeContracts.Fody
{
    public class ModuleWeaver
    {
        public ModuleDefinition ModuleDefinition { get; set; }

        public XElement Config { get; set; }
        
        public Action<string> LogDebug { get; set; }

        public Action<string> LogInfo { get; set; }
        
        public Action<string> LogWarning { get; set; }

        public Action<string> LogError { get; set; }

        public void Execute()
        {
            Contract.Requires(ModuleDefinition != null);
            Contract.Requires(Config != null);
            Contract.Requires(LogDebug != null);
            Contract.Requires(LogInfo != null);
            Contract.Requires(LogWarning != null);
            Contract.Requires(LogError != null);

            new TinyIoCConfiguration().Configure(this);
            new LoggerConfiguration().Configure(this);
            
            TinyIoCContainer.Current.Resolve<ContractExecutor>().Execute(ModuleDefinition);
        }
    }
}
