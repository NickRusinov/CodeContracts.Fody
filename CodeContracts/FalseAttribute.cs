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

        [Pure]
        public static bool Validate(bool arg) => !arg;
    }
}
