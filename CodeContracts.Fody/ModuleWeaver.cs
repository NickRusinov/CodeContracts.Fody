using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CodeContracts.Fody.Configurations;
using Mono.Cecil;
using TinyIoC;
using static System.StringComparer;

namespace CodeContracts.Fody
{
    /// <summary>
    /// Fody weaver addin for representation code contracts as custom attributes
    /// </summary>
    public class ModuleWeaver
    {
        /// <summary>
        /// An instance of Mono.Cecil.ModuleDefinition for processing
        /// </summary>
        public ModuleDefinition ModuleDefinition { get; set; }

        /// <summary>
        /// Contain the full element XML from FodyWeavers.xml
        /// </summary>
        public XElement Config { get; set; }

        /// <summary>
        /// Log an MessageImportance.Normal message to MSBuild
        /// </summary>
        public Action<string> LogDebug { get; set; }

        /// <summary>
        /// Log an MessageImportance.High message to MSBuild
        /// </summary>
        public Action<string> LogInfo { get; set; }

        /// <summary>
        /// Log an warning message to MSBuild
        /// </summary>
        public Action<string> LogWarning { get; set; }

        /// <summary>
        /// Log an error message to MSBuild
        /// </summary>
        public Action<string> LogError { get; set; }

        /// <summary>
        /// Will be called when an assembly will be processing
        /// </summary>
        public void Execute()
        {
            Contract.Requires(ModuleDefinition != null);
            Contract.Requires(Config != null);
            Contract.Requires(LogDebug != null);
            Contract.Requires(LogInfo != null);
            Contract.Requires(LogWarning != null);
            Contract.Requires(LogError != null);

#if DEBUG
            if (OrdinalIgnoreCase.Equals(Config.Attribute("IsDebug")?.Value, bool.TrueString) && !Debugger.IsAttached)
                Debugger.Launch();
#endif

            new TinyIoCConfiguration().Configure(this);
            new LoggerConfiguration().Configure(this);
            
            TinyIoCContainer.Current.Resolve<ContractExecutor>().Execute(ModuleDefinition);
        }
    }
}
