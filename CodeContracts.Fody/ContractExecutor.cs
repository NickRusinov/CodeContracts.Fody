using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractInjectResolvers;
using CodeContracts.Fody.ContractScanners;
using Mono.Cecil;

namespace CodeContracts.Fody
{
    public class ContractExecutor
    {
        private readonly IModuleScanner moduleScanner;

        private readonly IContractInjectResolver contractInjectResolver;

        private readonly IContractInjector contractInjector;

        public ContractExecutor(IModuleScanner moduleScanner, IContractInjectResolver contractInjectResolver, IContractInjector contractInjector)
        {
            Contract.Requires(moduleScanner != null);
            Contract.Requires(contractInjectResolver != null);
            Contract.Requires(contractInjector != null);

            this.moduleScanner = moduleScanner;
            this.contractInjectResolver = contractInjectResolver;
            this.contractInjector = contractInjector;
        }

        public void Execute(ModuleDefinition moduleDefinition)
        {
            foreach (var contractDefinition in moduleScanner.Scan(moduleDefinition))
            {
                var injectMethodDefinition = contractInjectResolver.Resolve(contractDefinition);
                contractInjector.Inject(contractDefinition, injectMethodDefinition);
            }
        }
    }
}
