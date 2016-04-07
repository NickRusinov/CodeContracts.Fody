using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody.Internal
{
    internal static class EnumerableUtils
    {
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] enumerables)
        {
            Contract.Requires(enumerables != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

            return enumerables.SelectMany(e => e);
        }
    }
}
