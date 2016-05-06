using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.Tests.Internal
{
    internal class InstructionComparer : IEqualityComparer<Instruction>
    {
        public static readonly IEqualityComparer<Instruction> Default = new InstructionComparer();

        public bool Equals(Instruction x, Instruction y) => Equals(x.OpCode, y.OpCode) && Equals(x.Operand, y.Operand);

        public int GetHashCode(Instruction obj) => obj.OpCode.GetHashCode() ^ obj.Operand?.GetHashCode() ?? 0;
    }
}
