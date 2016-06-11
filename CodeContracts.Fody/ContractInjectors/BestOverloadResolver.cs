using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class BestOverloadResolver : IBestOverloadResolver
    {
        private readonly IBestOverloadCriteria bestOverloadCriteria;

        public BestOverloadResolver(IBestOverloadCriteria bestOverloadCriteria)
        {
            Contract.Requires(bestOverloadCriteria != null);

            this.bestOverloadCriteria = bestOverloadCriteria;
        }

        public MethodReference Resolve(IReadOnlyCollection<MethodReference> methodReferences, IReadOnlyCollection<ParameterDefinition> parameterDefinitions)
        {
            methodReferences = methodReferences.Where(mr => bestOverloadCriteria.IsApply(mr, parameterDefinitions)).ToList();

            return methodReferences.Single(mr => methodReferences.All(imr => BestOverloadMethodComparer.Instance.Compare(mr, imr) >= 0));
        }
    }
}
