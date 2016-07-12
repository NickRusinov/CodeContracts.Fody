using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public sealed class NullAttribute : ContractAttribute
    {
        public NullAttribute() { }

        public NullAttribute(object arg) { }

        public NullAttribute(object arg0, object arg1) { }

        [Pure]
        public static bool Validate(object arg) => arg == null;

        [Pure]
        public static bool Validate(object arg0, object arg1) => arg0 == null && arg1 == null;
    }
}
