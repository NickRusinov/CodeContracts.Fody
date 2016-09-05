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
        /// <inheritdoc/>
        public ContractConfig Parse(string configString)
        {
            var xmlElement = XElement.Parse(configString);
            var xmlSerializer = new XmlSerializer(typeof(ContractConfig), new XmlRootAttribute(xmlElement.Name.LocalName));

            using (var stringReader = new StringReader(xmlElement.ToString()))
                return (ContractConfig)xmlSerializer.Deserialize(stringReader);
        }
    }
}
