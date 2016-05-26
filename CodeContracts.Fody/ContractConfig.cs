using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CodeContracts.Fody
{
    [XmlRoot, Serializable]
    public class ContractConfig
    {
        [XmlAttribute]
        public bool IsEnabled { get; set; } = true;

        [XmlAttribute]
        public RequiresMode Requires { get; set; } = RequiresMode.WithoutMessages | RequiresMode.WithoutExceptions;

        [XmlAttribute]
        public EnsuresMode Ensures { get; set; } = EnsuresMode.WithoutMessages;

        [XmlAttribute]
        public InvariantMode Invariant { get; set; } = InvariantMode.WithoutMessages;
    }

    [Flags, Serializable]
    public enum RequiresMode
    {
        WithoutMessages = 0,

        WithMessages = 1,

        WithoutExceptions = 0,

        WithExceptions = 2,
    }

    [Flags, Serializable]
    public enum EnsuresMode
    {
        WithoutMessages = 0,

        WithMessages = 1,
    }

    [Flags, Serializable]
    public enum InvariantMode
    {
        WithoutMessages = 0,

        WithMessages = 1,
    }
}
