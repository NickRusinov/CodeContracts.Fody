﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using static CodeContracts.Fody.Internal.ContractReferences;
using static CodeContracts.Fody.InvariantMode;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Creates a il instructions builder <see cref="IInstructionsBuilder"/> for injecting one 
    /// of <see cref="Contract"/>'s invariant methods
    /// </summary>
    public class ContractInvariantFactory : IContractMethodFactory
    {
        /// <summary>
        /// Definition of current weaving assembly
        /// </summary>
        private readonly ModuleDefinition moduleDefinition;

        /// <summary>
        /// Configuration of code contracts fody addin
        /// </summary>
        private readonly ContractConfig contractConfig;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractInvariantFactory"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <param name="contractConfig">Configuration of code contracts fody addin</param>
        public ContractInvariantFactory(ModuleDefinition moduleDefinition, ContractConfig contractConfig)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(contractConfig != null);

            this.moduleDefinition = moduleDefinition;
            this.contractConfig = contractConfig;
        }

        /// <inheritdoc/>
        public IInstructionsBuilder Create(TypeDefinition typeDefinition, string message)
        {
            if (message != null && contractConfig.Invariant.HasFlag(WithMessages))
                return new ContractMethodWithMessageBuilder(new ContractValidateBuilder(moduleDefinition), InvariantWithMessage(moduleDefinition), message);

            return new ContractMethodBuilder(new ContractValidateBuilder(moduleDefinition), Invariant(moduleDefinition));
        }
    }
}
