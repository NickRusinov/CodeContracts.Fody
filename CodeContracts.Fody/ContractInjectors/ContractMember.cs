using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public struct ContractMember : IEnumerable<ContractMember>
    {
        public ContractMember(ParameterDefinition parameterDefinition, IParameterBuilder parameterBuilder)
        {
            Contract.Requires(parameterDefinition != null);
            Contract.Requires(parameterBuilder != null);

            ParameterDefinition = parameterDefinition;
            ParameterBuilder = parameterBuilder;
        }

        public ParameterDefinition ParameterDefinition { get; }

        public IParameterBuilder ParameterBuilder { get; }

        public IEnumerator<ContractMember> GetEnumerator() => Enumerable.Repeat(this, 1).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
