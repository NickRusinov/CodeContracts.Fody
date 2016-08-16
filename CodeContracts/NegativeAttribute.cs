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
    public sealed class NegativeAttribute : ContractAttribute
    {
        public NegativeAttribute() { }

        public NegativeAttribute(object arg) { }

        [Pure]
        public static bool Validate(int arg) => arg <= 0;

        [Pure]
        public static bool Validate(uint arg) => arg <= 0u;

        [Pure]
        public static bool Validate(long arg) => arg <= 0L;

        [Pure]
        public static bool Validate(ulong arg) => arg <= 0uL;
    }
}
