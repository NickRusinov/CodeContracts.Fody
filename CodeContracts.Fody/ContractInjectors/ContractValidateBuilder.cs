﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.Internal;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractValidateBuilder : IInstructionsBuilder
    {
        private readonly ContractValidateDefinition contractValidateDefinition;

        public ContractValidateBuilder(ContractValidateDefinition contractValidateDefinition)
        {
            Contract.Requires(contractValidateDefinition != null);

            this.contractValidateDefinition = contractValidateDefinition;
        }

        public IEnumerable<Instruction> Build(IEnumerable<Instruction> instructions)
        {
            return EnumerableUtils.Concat(
                Enumerable.Repeat(Instruction.Create(OpCodes.Nop), 1),
                instructions,
                Enumerable.Repeat(Instruction.Create(OpCodes.Call, contractValidateDefinition.ValidateMethod), 1));
        }
    }
}
