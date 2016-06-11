﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public interface IBestOverloadResolver
    {
        MethodReference Resolve(IReadOnlyCollection<MethodReference> methodReferences, IReadOnlyCollection<ParameterDefinition> parameterDefinitions);
    }
}
