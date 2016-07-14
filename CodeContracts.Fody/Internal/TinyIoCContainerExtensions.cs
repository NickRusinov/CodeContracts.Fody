using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;

namespace CodeContracts.Fody.Internal
{
    /// <summary>
    /// Contains extension methods for <see cref="TinyIoCContainer"/>
    /// </summary>
    internal static class TinyIoCContainerExtensions
    {
        /// <summary>
        /// Resolves service to one of two implementations by configuration flag:
        /// first implementation if configuration flag is enabled and second implementation otherwise
        /// </summary>
        /// <typeparam name="TService">Type of resolved service</typeparam>
        /// <typeparam name="TTrueImplementation">Type of first implementation for enabled configuration flag</typeparam>
        /// <typeparam name="TFalseImplementation">Type of second implementation for disabled configuration flag</typeparam>
        /// <param name="container">Dependency injection container</param>
        /// <param name="configFunc">Function that gets configuration flag</param>
        /// <returns>One of two implementations</returns>
        public static TService ResolveByConfig<TService, TTrueImplementation, TFalseImplementation>(this TinyIoCContainer container, Func<ContractConfig, bool> configFunc)
            where TTrueImplementation : class, TService
            where TFalseImplementation : class, TService
        {
            Contract.Requires(container != null);
            Contract.Requires(configFunc != null);
            Contract.Ensures(Contract.Result<TService>() != null);

            if (configFunc.Invoke(container.Resolve<ContractConfig>()))
                return container.Resolve<TTrueImplementation>();

            return container.Resolve<TFalseImplementation>();
        }
    }
}
