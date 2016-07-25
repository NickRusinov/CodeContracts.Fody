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
    public class IsAttribute : ContractAttribute
    {
        public IsAttribute(object type) { }

        public IsAttribute(object arg, object type) { }

        [Pure]
        public static bool Validate(object arg, Type type) => type?.IsAssignableFrom(arg?.GetType()) ?? false;
    }
}
