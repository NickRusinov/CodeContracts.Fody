using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public class ThisAttribute : ContractAttribute
    {
        public ThisAttribute(params object[] args)
            : base(args)
        {
            
        }

        [Pure]
        public static bool Validate(object self, object arg) => ReferenceEquals(self, arg);
    }
}
