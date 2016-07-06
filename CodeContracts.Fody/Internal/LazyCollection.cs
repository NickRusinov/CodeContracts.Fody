using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody.Internal
{
    /// <summary>
    /// Represents collection that initializes only by calls its enumerator
    /// </summary>
    /// <typeparam name="T">Type of items in collection</typeparam>
    internal class LazyCollection<T> : IReadOnlyCollection<T>
    {
        /// <summary>
        /// <see cref="Lazy{T}"/> object that initializes collection
        /// </summary>
        private readonly Lazy<IReadOnlyCollection<T>> lazyCollection;

        /// <summary>
        /// Initializes a new instance of class <see cref="LazyCollection{T}"/>
        /// </summary>
        /// <param name="lazyCollection"><see cref="Lazy{T}"/> object that initializes collection</param>
        public LazyCollection(Lazy<IReadOnlyCollection<T>> lazyCollection)
        {
            Contract.Requires(lazyCollection != null);

            this.lazyCollection = lazyCollection;
        }

        /// <summary>
        /// Initializes a new instance of class <see cref="LazyCollection{T}"/>
        /// </summary>
        /// <param name="funcCollection">Function that initializes collection</param>
        public LazyCollection(Func<IReadOnlyCollection<T>> funcCollection)
            : this(new Lazy<IReadOnlyCollection<T>>(funcCollection))
        {
            Contract.Requires(funcCollection != null);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// First call causes initialization current collection
        /// </remarks>
        public int Count => lazyCollection.Value.Count;

        /// <inheritdoc/>
        /// <remarks>
        /// First call causes initialization current collection
        /// </remarks>
        public IEnumerator<T> GetEnumerator() => lazyCollection.Value.GetEnumerator();

        /// <inheritdoc/>
        /// <remarks>
        /// First call causes initialization current collection
        /// </remarks>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
