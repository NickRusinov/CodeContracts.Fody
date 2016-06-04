using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.TestAssembly
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentOutOfRangeException))]
    public sealed class MyAttribute : ContractAttribute
    {
        public MyAttribute(params object[] args)
            : base(args)
        {

        }

        public object Min { get; set; }

        public object Max { get; set; }

        [Pure]
        public static bool ValidateMinInt(object self, int arg, int min) => arg >= min;

        [Pure]
        public static bool ValidateMaxInt(object self, int arg, int max) => arg <= max;

        [Pure]
        public static bool ValidateMinMaxInt(object self, int arg, int min, int max) => arg >= min && arg <= max;

        [Pure]
        public static bool ValidateMinLong(object self, long arg, long min) => arg >= min;

        [Pure]
        public static bool ValidateMaxLong(object self, long arg, long max) => arg <= max;

        [Pure]
        public static bool ValidateMinMaxLong(object self, long arg, long min, long max) => arg >= min && arg <= max;

        [Pure]
        public static bool ValidateMinComparable(object self, IComparable arg, IComparable min) => arg.CompareTo(min) >= 0;

        [Pure]
        public static bool ValidateMaxComparable(object self, IComparable arg, IComparable max) => arg.CompareTo(max) <= 0;

        [Pure]
        public static bool ValidateMinMaxComparable(object self, IComparable arg, IComparable min, IComparable max) => arg.CompareTo(min) >= 0 && arg.CompareTo(max) <= 0;
    }
}
