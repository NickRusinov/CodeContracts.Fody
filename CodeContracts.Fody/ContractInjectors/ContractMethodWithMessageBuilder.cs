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
    public class ContractMethodWithMessageBuilder : IInstructionsBuilder
    {
        private readonly MethodReference methodReference;

        private readonly string message;

        public ContractMethodWithMessageBuilder(MethodReference methodReference, string message)
        {
            Contract.Requires(methodReference != null);
            Contract.Requires(message != null);

            this.methodReference = methodReference;
            this.message = message;
        }

        public IEnumerable<Instruction> Build(IEnumerable<Instruction> instructions)
        {
            return EnumerableUtils.Concat(
                Enumerable.Repeat(Instruction.Create(OpCodes.Nop), 1),
                instructions,
                Enumerable.Repeat(Instruction.Create(OpCodes.Ldstr, message), 1),
                Enumerable.Repeat(Instruction.Create(OpCodes.Call, methodReference), 1));
        }
    }
}
