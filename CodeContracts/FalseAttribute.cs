using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public sealed class FalseAttribute : ContractAttribute
    {
        public FalseAttribute() { }

        public FalseAttribute(object arg) { }

        public FalseAttribute(object arg0, object arg1) { }

        [Pure]
        public static bool Validate(bool arg) => !arg;

        [Pure]
        public static bool Validate(bool arg0, bool arg1) => !arg0 && !arg1;
    }
}
