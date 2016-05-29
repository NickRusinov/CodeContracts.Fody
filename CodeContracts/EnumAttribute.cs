using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public class EnumAttribute : ContractAttribute
    {
        public EnumAttribute(params object[] args)
            : base(args)
        {
            
        }

        [Pure]
        public static bool Validate(object self, Enum arg) => Enum.IsDefined(arg.GetType(), arg);
    }
}
