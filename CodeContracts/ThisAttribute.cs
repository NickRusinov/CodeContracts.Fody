﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace CodeContracts
{
    [Conditional("CONTRACTS_FULL")]
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public sealed class ThisAttribute : ContractAttribute
    {
        public ThisAttribute() { }

        public ThisAttribute(object arg) { }

        [Pure]
        public static bool Validate(object self, object arg) => self != null && arg != null && ReferenceEquals(self, arg);
    }
}
