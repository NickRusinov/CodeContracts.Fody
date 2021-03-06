﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractCleaners;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractInjectResolvers;
using CodeContracts.Fody.ContractScanners;
using CodeContracts.Fody.Exceptions;
using Mono.Cecil;

namespace CodeContracts.Fody
{
    /// <summary>
    /// Top level algorithm for replace custom contract attributes to <see cref="Contract"/>'s methods calls
    /// </summary>
    public class ContractExecutor : IContractExecutor
    {
        /// <summary>
        /// Scans custom contract attributes in a assembly
        /// </summary>
        private readonly IModuleScanner moduleScanner;

        /// <summary>
        /// Resolves method in which will be injected contract expressions
        /// </summary>
        private readonly IContractInjectResolver contractInjectResolver;

        /// <summary>
        /// Injectes calls of methods of <see cref="Contract"/> class to specified methods
        /// </summary>
        private readonly IContractInjector contractInjector;

        /// <summary>
        /// Removes custom contract attribute from assembly
        /// </summary>
        private readonly IContractCleaner contractCleaner;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractExecutor"/>
        /// </summary>
        /// <param name="moduleScanner">Scans custom contract attributes in a assembly</param>
        /// <param name="contractInjectResolver">Resolves method in which will be injected contract expressions</param>
        /// <param name="contractInjector">Injectes calls of methods of <see cref="Contract"/> class to specified methods</param>
        /// <param name="contractCleaner">Removes custom contract attribute from assembly</param>
        public ContractExecutor(IModuleScanner moduleScanner, IContractInjectResolver contractInjectResolver, IContractInjector contractInjector, IContractCleaner contractCleaner)
        {
            Contract.Requires(moduleScanner != null);
            Contract.Requires(contractInjectResolver != null);
            Contract.Requires(contractInjector != null);
            Contract.Requires(contractCleaner != null);

            this.moduleScanner = moduleScanner;
            this.contractInjectResolver = contractInjectResolver;
            this.contractInjector = contractInjector;
            this.contractCleaner = contractCleaner;
        }

        /// <inheritdoc/>
        public void Execute(ModuleDefinition moduleDefinition)
        {
            foreach (var contractDefinition in moduleScanner.Scan(moduleDefinition).ToList())
                try
                {
                    var injectMethodDefinition = contractInjectResolver.Resolve(contractDefinition);

                    contractInjector.Inject(contractDefinition, injectMethodDefinition);
                    contractCleaner.Clean(contractDefinition);
                }
                catch (CodeContractsFodyException e) when (e.Level == TraceLevel.Error)
                {
                    Logger.Instance.LogError(e.Message);
                }
                catch (CodeContractsFodyException e) when (e.Level == TraceLevel.Warning)
                {
                    Logger.Instance.LogWarning(e.Message);
                }
                catch (CodeContractsFodyException e) when (e.Level == TraceLevel.Info)
                {
                    Logger.Instance.LogInfo(e.Message);
                }
        }
    }
}
