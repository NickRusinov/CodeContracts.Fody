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
    public class BestOverloadParameterComparer : IComparer<ParameterDefinition>
    {
        public static BestOverloadParameterComparer Instance { get; } = new BestOverloadParameterComparer();

        public int Compare(ParameterDefinition x, ParameterDefinition y)
        {
            if (TypeReferenceComparer.Instance.Equals(x.ParameterType, y.ParameterType))
                return 0;

            if (x.ParameterType.IsAssignable(y.ParameterType) ||
                x.ParameterType.IsSignedNumeric() && y.ParameterType.IsUnsignedNumeric())
                return +1;

            if (y.ParameterType.IsAssignable(x.ParameterType) ||
                y.ParameterType.IsSignedNumeric() && x.ParameterType.IsUnsignedNumeric())
                return -1;

            return 0;
        }
    }
}
