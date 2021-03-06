﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.Exceptions;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.BestOverloadResolvers
{
    /// <summary>
    /// Resolves method that is a best overload for specified parameters
    /// </summary>
    public class BestOverloadResolver : IBestOverloadResolver
    {
        /// <summary>
        /// Criteria that define can specified method overload calls with specified parameters
        /// </summary>
        private readonly IBestOverloadCriteria bestOverloadCriteria;

        /// <summary>
        /// Initializes a new instance of class <see cref="BestOverloadResolver"/>
        /// </summary>
        /// <param name="bestOverloadCriteria">
        /// Criteria that define can specified method overload calls with specified parameters
        /// </param>
        public BestOverloadResolver(IBestOverloadCriteria bestOverloadCriteria)
        {
            Contract.Requires(bestOverloadCriteria != null);

            this.bestOverloadCriteria = bestOverloadCriteria;
        }

        /// <inheritdoc/>
        public MethodReference Resolve(IReadOnlyCollection<MethodReference> methodReferences, IReadOnlyCollection<ParameterDefinition> parameterDefinitions)
        {
            var appliedMethodReferences = methodReferences
                .Where(mr => bestOverloadCriteria.IsApply(mr, parameterDefinitions))
                .ToList();

            return appliedMethodReferences
                .Single(mr => appliedMethodReferences.All(imr => BestOverloadMethodComparer.Instance.Compare(mr, imr) >= 0),
                    ms => new BestOverloadAmbiguousMethodsException(methodReferences, ms),
                    () => new BestOverloadMissingMethodsException(methodReferences));
        }
    }
}
