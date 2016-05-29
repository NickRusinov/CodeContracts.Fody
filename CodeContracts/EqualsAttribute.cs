using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public class EqualsAttribute : ContractAttribute
    {
        public EqualsAttribute(params object[] args)
            : base(args)
        {
            
        }

        public object To { get; set; }

        [Pure]
        public static bool Validate(object self, object arg, object to) => Equals(arg, to);

        [Pure]
        public static bool Validate(object self, int arg, int to) => arg.Equals(to);
    }
}
