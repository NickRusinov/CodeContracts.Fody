using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentOutOfRangeException))]
    public sealed class RangeAttribute : ContractAttribute
    {
        public RangeAttribute(object min, object max) { }

        public RangeAttribute(object arg, object min, object max) { }

        public RangeAttribute(object arg0, object arg1, object min, object max) { }

        [Pure]
        public static bool Validate(IComparable arg, IComparable min, IComparable max) => arg.CompareTo(min) >= 0 && arg.CompareTo(max) <= 0;

        [Pure]
        public static bool Validate(IComparable arg0, IComparable arg1, IComparable min, IComparable max) => arg0.CompareTo(min) >= 0 && arg0.CompareTo(max) <= 0 && arg1.CompareTo(min) >= 0 && arg1.CompareTo(max) <= 0;

        [Pure]
        public static bool Validate(int arg, int min, int max) => arg >= min && arg <= max;

        [Pure]
        public static bool Validate(int arg0, int arg1, int min, int max) => arg0 >= min && arg1 <= max && arg1 >= min && arg1 <= max;
    }
}
