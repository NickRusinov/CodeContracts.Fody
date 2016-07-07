using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody.Internal
{
    /// <summary>
    /// Contains extension methods for collection interfaces such as <see cref="IEnumerable{T}"/>, 
    /// <see cref="ICollection{T}"/>, <see cref="IList{T}"/> and etc.
    /// </summary>
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Concatanates all items in specified collections to one collection
        /// </summary>
        /// <typeparam name="T">Type of items in collections</typeparam>
        /// <param name="enumerables">Collections that will be concatanated</param>
        /// <returns>Concatanated collection</returns>
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] enumerables)
        {
            Contract.Requires(enumerables != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

            return enumerables.SelectMany(e => e);
        }

        /// <summary>
        /// Adds collection of items to end of specified list
        /// </summary>
        /// <typeparam name="T">Type of items in collections</typeparam>
        /// <param name="list">List in that will be added items</param>
        /// <param name="items">Collection of items</param>
        /// <returns><paramref name="list"/> reference</returns>
        public static IList<T> AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            Contract.Requires(list != null);
            Contract.Requires(items != null);
            Contract.Ensures(ReferenceEquals(Contract.Result<IList<T>>(), list));

            foreach (var item in items)
                list.Add(item);

            return list;
        }

        /// <summary>
        /// Adds collection of items to begin of specified list
        /// </summary>
        /// <typeparam name="T">Type of items in collections</typeparam>
        /// <param name="list">List in that will be added items</param>
        /// <param name="index">The zero-based index at which item should be inserted</param>
        /// <param name="items">Collection of items</param>
        /// <returns><paramref name="list"/> reference</returns>
        public static IList<T> InsertRange<T>(this IList<T> list, int index, IEnumerable<T> items)
        {
            Contract.Requires(list != null);
            Contract.Requires(items != null);
            Contract.Requires(index >= 0 && index <= list.Count);
            Contract.Ensures(ReferenceEquals(Contract.Result<IList<T>>(), list));
            
            foreach (var item in items)
                list.Insert(index++, item);

            return list;
        }
    }
}
