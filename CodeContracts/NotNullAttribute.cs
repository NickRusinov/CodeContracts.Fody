using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class NotNullAttribute : ContractAttribute
    {
        public NotNullAttribute(params object[] args)
        {

        }

        public static bool Validate(object self, object arg) => arg != null;
    }
}
