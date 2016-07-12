using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentNullException))]
    public sealed class NotNullAttribute : ContractAttribute
    {
        public NotNullAttribute() { }

        public NotNullAttribute(object arg) { }

        public NotNullAttribute(object arg0, object arg1) { }

        [Pure]
        public static bool Validate(object arg) => arg != null;

        [Pure]
        public static bool Validate(object arg0, object arg1) => arg0 != null && arg1 != null;
    }
}
