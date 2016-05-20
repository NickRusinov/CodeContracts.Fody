using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class RangeAttribute : ContractAttribute
    {
        public RangeAttribute(params object[] args)
        {

        }

        public object Min { get; set; }

        public object Max { get; set; }

        public static bool ValidateMin(object self, int arg, int min) => arg >= min;

        public static bool ValidateMax(object self, int arg, int max) => arg <= max;

        public static bool ValidateMinMax(object self, int arg, int min, int max) => arg >= min && arg <= max;
    }
}
