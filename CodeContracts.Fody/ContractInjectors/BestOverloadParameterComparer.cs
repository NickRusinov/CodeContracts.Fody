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
    /// <summary>
    /// Defines priority of method's parameters
    /// </summary>
    public class BestOverloadParameterComparer : IComparer<ParameterDefinition>
    {
        /// <summary>
        /// Singleton <see cref="BestOverloadParameterComparer"/> comparer
        /// </summary>
        public static BestOverloadParameterComparer Instance { get; } = new BestOverloadParameterComparer();

        /// <inheritdoc/>
        /// <remarks>
        /// Returns +1 then <paramref name="x"/> has more priority then <paramref name="y"/>; -1 otherwise.
        /// 0 then <paramref name="x"/> and <paramref name="y"/> independent
        /// </remarks>
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
