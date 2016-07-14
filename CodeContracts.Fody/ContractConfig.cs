using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeContracts.Fody
{
    /// <summary>
    /// Configuration of code contracts fody addin
    /// </summary>
    [XmlRoot, Serializable]
    public class ContractConfig
    {
        /// <summary>
        /// If disabled then assembly will not be rewritten like addin will not be.
        /// Default value is enabled
        /// </summary>
        [XmlAttribute]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// If enabled then all contract attributes will be removed from final assembly.
        /// Default value is enabled
        /// </summary>
        [XmlAttribute]
        public bool Clean { get; set; } = true;

        /// <summary>
        /// Determining the mode of implementation Contract.Requires overload.
        /// Default value is without messages and without specified exception type overload
        /// </summary>
        [XmlAttribute]
        public RequiresMode Requires { get; set; } = RequiresMode.WithoutMessages | RequiresMode.WithoutExceptions;

        /// <summary>
        /// Determining the mode of implementation Contract.Ensures overload.
        /// Default value is without messages overload
        /// </summary>
        [XmlAttribute]
        public EnsuresMode Ensures { get; set; } = EnsuresMode.WithoutMessages;

        /// <summary>
        /// Determining the mode of implementation Contract.Invariant overload.
        /// Default value is without messages overload
        /// </summary>
        [XmlAttribute]
        public InvariantMode Invariant { get; set; } = InvariantMode.WithoutMessages;
    }

    /// <summary>
    /// Determining the mode of implementation Contract.Requires overload
    /// </summary>
    [Flags, Serializable]
    public enum RequiresMode
    {
        /// <summary>
        /// Use <see cref="Contract.Requires(bool)"/> or <see cref="Contract.Requires{TException}(bool)"/>
        /// without message overload
        /// </summary>
        WithoutMessages = 0,

        /// <summary>
        /// Use <see cref="Contract.Requires(bool, string)"/> or <see cref="Contract.Requires{TException}(bool, string)"/>
        /// with message overload
        /// </summary>
        WithMessages = 1,

        /// <summary>
        /// Use <see cref="Contract.Requires(bool)"/> or <see cref="Contract.Requires(bool, string)"/> without
        /// specified exception type overload
        /// </summary>
        WithoutExceptions = 0,

        /// <summary>
        /// Use <see cref="Contract.Requires{TException}(bool)"/> or <see cref="Contract.Requires{TException}(bool, string)"/>
        /// with specified exception type overload
        /// </summary>
        WithExceptions = 2,
    }

    /// <summary>
    /// Determining the mode of implementation Contract.Ensures overload
    /// </summary>
    [Flags, Serializable]
    public enum EnsuresMode
    {
        /// <summary>
        /// Use <see cref="Contract.Ensures(bool)"/> without message overload
        /// </summary>
        WithoutMessages = 0,

        /// <summary>
        /// Use <see cref="Contract.Ensures(bool, string)"/> with message overload
        /// </summary>
        WithMessages = 1,
    }

    /// <summary>
    /// Determining the mode of implementation Contract.Invariant overload
    /// </summary>
    [Flags, Serializable]
    public enum InvariantMode
    {
        /// <summary>
        /// Use <see cref="Contract.Invariant(bool)"/> without message overload
        /// </summary>
        WithoutMessages = 0,

        /// <summary>
        /// Use <see cref="Contract.Invariant(bool, string)"/> with message overload
        /// </summary>
        WithMessages = 1,
    }
}
