using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.Tests.Internal
{
    public class CallInstructionComparer : IEqualityComparer<Instruction>
    {
        public static IEqualityComparer<Instruction> Instance { get; } = new CallInstructionComparer();

        public bool Equals(Instruction x, Instruction y) => Equals(x.OpCode, y.OpCode) && MethodReferenceComparer.Instance.Equals((MethodReference)x.Operand, (MethodReference)y.Operand);

        public int GetHashCode(Instruction obj) => obj.OpCode.GetHashCode() ^ ((MethodReference)obj.Operand).Resolve().GetHashCode();
    }
}
