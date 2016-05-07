﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class NullParameterBuilder : IInstructionsBuilder
    {
        public IEnumerable<Instruction> Build(IEnumerable<Instruction> instructions)
        {
            return EnumerableUtils.Concat(
                Enumerable.Repeat(Instruction.Create(OpCodes.Nop), 1),
                instructions,
                Enumerable.Repeat(Instruction.Create(OpCodes.Ldnull), 1));
        }
    }
}
