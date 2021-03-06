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
    [ContractException(typeof(ArgumentNullException))]
    public sealed class NotNullAttribute : ContractAttribute
    {
        public NotNullAttribute() { }

        public NotNullAttribute(object arg) { }

        [Pure]
        public static bool Validate(object arg) => arg != null;
    }
}
