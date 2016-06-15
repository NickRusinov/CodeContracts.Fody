using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody.Internal
{
    internal class LazyCollection<T> : IReadOnlyCollection<T>
    {
        private readonly Lazy<IReadOnlyCollection<T>> lazyCollection;

        public LazyCollection(Lazy<IReadOnlyCollection<T>> lazyCollection)
        {
            Contract.Requires(lazyCollection != null);

            this.lazyCollection = lazyCollection;
        }

        public LazyCollection(Func<IReadOnlyCollection<T>> funcCollection)
            : this(new Lazy<IReadOnlyCollection<T>>(funcCollection))
        {
            Contract.Requires(funcCollection != null);
        }

        public int Count => lazyCollection.Value.Count;

        public IEnumerator<T> GetEnumerator() => lazyCollection.Value.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
