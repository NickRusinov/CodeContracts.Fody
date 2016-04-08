using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.Fody.ContractDefinitions
{
    public class ContractParameterDefinition
    {
        public ContractParameterDefinition(string parameterName, object parameterValue)
        {
            Contract.Requires(parameterName != null);

            ParameterName = parameterName;
            ParameterValue = parameterValue;
        }

        public string ParameterName { get; }

        public object ParameterValue { get; }
    }
}
