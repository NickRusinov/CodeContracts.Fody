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

        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            Contract.Requires(collection != null);
            Contract.Requires(items != null);
            Contract.Ensures(ReferenceEquals(Contract.Result<ICollection<T>>(), collection));

            foreach (var item in items)
                collection.Add(item);

            return collection;
        }
    }
}
