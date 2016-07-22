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
    [ContractException(typeof(ArgumentException))]
    public sealed class EqualsAttribute : ContractAttribute
    {
        public EqualsAttribute(object to) { }

        public EqualsAttribute(object arg, object to) { }

        [Pure]
        public static bool Validate(object arg, object to) => Equals(arg, to);

        [Pure]
        public static bool Validate(int arg, int to) => arg == to;
    }
}
