using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public class RangeAttribute : ContractAttribute
    {
        public RangeAttribute(params object[] args)
            : base(args)
        {
            
        }

        public object Min { get; set; }

        public object Max { get; set; }

        public static bool ValidateMin(object self, int arg, int min) => arg >= min;

        public static bool ValidateMax(object self, int arg, int max) => arg <= max;

        public static bool ValidateMinMax(object self, int arg, int min, int max) => arg >= min && arg <= max;
    }
}
