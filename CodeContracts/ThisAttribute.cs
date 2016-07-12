using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public sealed class ThisAttribute : ContractAttribute
    {
        public ThisAttribute() { }

        public ThisAttribute(object arg) { }

        public ThisAttribute(object arg0, object arg1) { }

        [Pure]
        public static bool Validate(object self, object arg) => ReferenceEquals(self, arg);
        
        [Pure]
        public static bool Validate(object self, object arg0, object arg1) => ReferenceEquals(self, arg0) && ReferenceEquals(self, arg1);
    }
}
