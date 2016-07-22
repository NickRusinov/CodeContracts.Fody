using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [Conditional("CONTRACTS_FULL")]
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentOutOfRangeException))]
    public sealed class RangeAttribute : ContractAttribute
    {
        public RangeAttribute(object min, object max) { }

        public RangeAttribute(object arg, object min, object max) { }

        [Pure]
        public static bool Validate(IComparable arg, IComparable min, IComparable max) => arg != null && arg.CompareTo(min) >= 0 && arg.CompareTo(max) <= 0;

        [Pure]
        public static bool Validate(int arg, int min, int max) => arg >= min && arg <= max;
    }
}
