using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Injectes calls of methods of <see cref="Contract"/> class to specified methods
    /// </summary>
    public interface IContractInjector
    {
        /// <summary>
        /// Injectes calls of methods of <see cref="Contract"/> class to specified methods
        /// </summary>
        /// <param name="contractDefinition">Contract definition for injecting to method</param>
        /// <param name="methodDefinition">Method in that will be injected il instructions</param>
        void Inject(ContractDefinition contractDefinition, MethodDefinition methodDefinition);
    }
}
