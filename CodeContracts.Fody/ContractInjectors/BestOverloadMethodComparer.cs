using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class BestOverloadMethodComparer : IComparer<MethodReference>
    {
        public static BestOverloadMethodComparer Instance { get; } = new BestOverloadMethodComparer();

        public int Compare(MethodReference x, MethodReference y)
        {
            var sharedParameterNames = x.Parameters.Select(pd => pd.Name).Intersect(y.Parameters.Select(pd => pd.Name)).ToList();
            var sharedParameterCompares = sharedParameterNames.Select(s => BestOverloadParameterComparer.Instance.Compare(x.Parameters.Single(pd => pd.Name == s), y.Parameters.Single(pd => pd.Name == s))).ToList();
            
            if (MethodReferenceComparer.Instance.Equals(x, y) ||
                sharedParameterNames.Count != x.Parameters.Count && sharedParameterNames.Count != y.Parameters.Count)
                return 0;

            if (sharedParameterCompares.All(i => i >= 0) && sharedParameterCompares.Any(i => i > 0) ||
                sharedParameterCompares.All(i => i == 0) && y.Parameters.Count > sharedParameterNames.Count)
                return +1;

            if (sharedParameterCompares.All(i => i <= 0) && sharedParameterCompares.Any(i => i < 0) ||
                sharedParameterCompares.All(i => i == 0) && x.Parameters.Count > sharedParameterNames.Count)
                return -1;

            return 0;
        }
    }
}
