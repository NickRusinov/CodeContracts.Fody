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
    public class ContractConfigParser : IContractConfigParser
    {
        private readonly XmlSerializer xmlSerializer = new XmlSerializer(typeof(ContractConfig), new XmlRootAttribute("CodeContracts"));

        public ContractConfig Parse(string configString)
        {
            using (var stringReader = new StringReader(XElement.Parse(configString).ToString()))
                return (ContractConfig)xmlSerializer.Deserialize(stringReader);
        }
    }
}
