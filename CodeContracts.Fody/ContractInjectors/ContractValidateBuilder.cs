using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractValidateBuilder : IInstructionsBuilder
    {
        private readonly ModuleDefinition moduleDefinition;

        public ContractValidateBuilder(ModuleDefinition moduleDefinition)
        {
            Contract.Requires(moduleDefinition != null);

            this.moduleDefinition = moduleDefinition;
        }

        public IEnumerable<Instruction> Build(ContractValidate contractValidate)
        {
            return EnumerableExtensions.Concat(
                Enumerable.Repeat(Instruction.Create(OpCodes.Nop), 1),
                contractValidate.ParameterBuilders.SelectMany((pb, i) => pb.Build(contractValidate.ValidateDefinition.ValidateMethod.Parameters[i])),
                Enumerable.Repeat(Instruction.Create(OpCodes.Call, moduleDefinition.ImportReference(contractValidate.ValidateDefinition.ValidateMethod)), 1));
        }
    }
}
