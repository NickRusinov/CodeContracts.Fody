﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    public interface IParameterBuilder
    {
        TypeReference ParameterType { get; }

        IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition);
    }
}
