using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody.ContractDefinitions
{
    public class ContractPropertyDefinition
    {
        public ContractPropertyDefinition(string propertyName, object propertyValue)
        {
            Contract.Requires(propertyName != null);

            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }

        public string PropertyName { get; }

        public object PropertyValue { get; }
    }
}
