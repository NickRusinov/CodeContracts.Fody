using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public sealed class EnumAttribute : ContractAttribute
    {
        public EnumAttribute() { }

        public EnumAttribute(object arg) { }

        public EnumAttribute(object arg0, object arg1) { }

        [Pure]
        public static bool Validate(Enum arg) => Enum.IsDefined(arg.GetType(), arg);

        [Pure]
        public static bool Validate(Enum arg0, Enum arg1) => Enum.IsDefined(arg0.GetType(), arg0) && Enum.IsDefined(arg1.GetType(), arg1);
    }
}
