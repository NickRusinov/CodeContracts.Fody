using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;

namespace CodeContracts.Fody.ContractInjectors
{
    public struct ContractValidate : IEnumerable<ContractValidate>
    {
        public ContractValidate(ContractValidateDefinition validateDefinition, IEnumerable<IParameterBuilder> parameterBuilders)
        {
            Contract.Requires(validateDefinition != null);
            Contract.Requires(parameterBuilders != null);

            ValidateDefinition = validateDefinition;
            ParameterBuilders = parameterBuilders;
        }

        public ContractValidateDefinition ValidateDefinition { get; }

        public IEnumerable<IParameterBuilder> ParameterBuilders { get; }

        public IEnumerator<ContractValidate> GetEnumerator() => Enumerable.Repeat(this, 1).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
