using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInstructionsBuilders
{
    /// <summary>
    /// Creates il instructions for inject call validation method of contract attribute
    /// </summary>
    public class ContractValidateBuilder : IInstructionsBuilder
    {
        /// <summary>
        /// Definition of current weaving assembly
        /// </summary>
        private readonly ModuleDefinition moduleDefinition;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractValidateBuilder"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        public ContractValidateBuilder(ModuleDefinition moduleDefinition)
        {
            Contract.Requires(moduleDefinition != null);

            this.moduleDefinition = moduleDefinition;
        }

        /// <inheritdoc/>
        public IEnumerable<Instruction> Build(ContractValidate contractValidate)
        {
            return EnumerableExtensions.Concat(
                Enumerable.Repeat(Instruction.Create(OpCodes.Nop), 1),
                contractValidate.ParameterBuilders.SelectMany((pb, i) => pb.Build(contractValidate.ValidateDefinition.ValidateMethod.Parameters[i])),
                Enumerable.Repeat(Instruction.Create(OpCodes.Call, moduleDefinition.ImportReference(contractValidate.ValidateDefinition.ValidateMethod)), 1));
        }
    }
}
