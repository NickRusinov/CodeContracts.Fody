using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeContracts
{
    [Conditional("CONTRACTS_FULL")]
    [AttributeUsage(DefaultUsages, AllowMultiple = true, Inherited = false)]
    [ContractException(typeof(ArgumentException))]
    public sealed class RegexAttribute : ContractAttribute
    {
        public RegexAttribute(object regex) { }

        public RegexAttribute(object arg, object regex) { }

        [Pure]
        public static bool Validate(string arg, string regex) => Regex.IsMatch(arg, regex);
    }
}
