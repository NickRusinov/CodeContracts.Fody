using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CodeContracts.Fody
{
    /// <summary>
    /// Parses string to object that represent configuration of code contracts fody addin
    /// where parsed string is xml tag
    /// </summary>
    public class ContractConfigParser : IContractConfigParser
    {
        /// <summary>
        /// <see cref="XmlSerializer"/> that deserialize given configuration string
        /// </summary>
        private readonly XmlSerializer xmlSerializer = new XmlSerializer(typeof(ContractConfig), new XmlRootAttribute("CodeContracts"));

        /// <inheritdoc/>
        public ContractConfig Parse(string configString)
        {
            using (var stringReader = new StringReader(XElement.Parse(configString).ToString()))
                return (ContractConfig)xmlSerializer.Deserialize(stringReader);
        }
    }
}
