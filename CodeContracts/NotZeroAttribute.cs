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
    [ContractException(typeof(ArgumentOutOfRangeException))]
    public class NotZeroAttribute : ContractAttribute
    {
        public NotZeroAttribute() { }

        public NotZeroAttribute(object arg) { }

        [Pure]
        public static bool Validate(int arg) => arg != 0;
    }
}