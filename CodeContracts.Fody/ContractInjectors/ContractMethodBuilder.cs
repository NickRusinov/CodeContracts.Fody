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
    public class ContractMethodBuilder : IInstructionsBuilder
    {
        private readonly IInstructionsBuilder instructionsBuilder;

        private readonly MethodReference methodReference;

        public ContractMethodBuilder(IInstructionsBuilder instructionsBuilder, MethodReference methodReference)
        {
            Contract.Requires(methodReference != null);
            Contract.Requires(instructionsBuilder != null);

            this.instructionsBuilder = instructionsBuilder;
            this.methodReference = methodReference;
        }

        public IEnumerable<Instruction> Build(ContractValidate contractValidate)
        {
            return EnumerableExtensions.Concat(
                Enumerable.Repeat(Instruction.Create(OpCodes.Nop), 1),
                instructionsBuilder.Build(contractValidate),
                Enumerable.Repeat(Instruction.Create(OpCodes.Call, methodReference), 1));
        }
    }
}
