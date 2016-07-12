using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public sealed class TrueAttribute : ContractAttribute
    {
        public TrueAttribute() { }

        public TrueAttribute(object arg) { }

        public TrueAttribute(object arg0, object arg1) { }

        [Pure]
        public static bool Validate(bool arg) => arg;

        [Pure]
        public static bool Validate(bool arg0, bool arg1) => arg0 && arg1;
    }
}
