using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public sealed class EqualsAttribute : ContractAttribute
    {
        public EqualsAttribute(object to) { }

        public EqualsAttribute(object arg, object to) { }

        public EqualsAttribute(object arg0, object arg1, object to) { }

        [Pure]
        public static bool Validate(object arg, object to) => Equals(arg, to);

        [Pure]
        public static bool Validate(object arg0, object arg1, object to) => Equals(arg0, to) && Equals(arg1, to);

        [Pure]
        public static bool Validate(int arg, int to) => arg == to;

        [Pure]
        public static bool Validate(int arg0, int arg1, int to) => arg0 == to && arg1 == to;
    }
}
