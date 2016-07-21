﻿using System;
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

        [Pure]
        public static bool Validate(Enum arg) => Enum.IsDefined(arg.GetType(), arg);
    }
}
