using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
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
            methodReferences = methodReferences.Where(mr => bestOverloadCriteria.IsApply(mr, parameterDefinitions)).ToList();

            return methodReferences.Single(mr => methodReferences.All(imr => BestOverloadMethodComparer.Instance.Compare(mr, imr) >= 0));
        }
    }
}
