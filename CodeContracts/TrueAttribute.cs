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
        public TrueAttribute(params object[] args)
            : base(args)
        {

        }

        [Pure]
        public static bool Validate(object self, bool arg) => arg;
    }
}
