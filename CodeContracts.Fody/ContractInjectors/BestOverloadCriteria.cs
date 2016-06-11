﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class BestOverloadCriteria : IBestOverloadCriteria
    {
        public bool IsApply(MethodReference methodReference, IReadOnlyCollection<ParameterDefinition> parameterDefinitions)
        {
            return methodReference.Parameters.All(pd => parameterDefinitions.Select(ipd => ipd.Name).Contains(pd.Name)) &&
                   parameterDefinitions.Where(pd => !pd.IsOptional).All(pd => methodReference.Parameters.Select(ipd => ipd.Name).Contains(pd.Name)) &&
                   methodReference.Parameters.All(pd => parameterDefinitions.Single(ipd => ipd.Name == pd.Name).ParameterType.IsAssignable(pd.ParameterType));
        }
    }
}