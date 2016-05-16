using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class CompositeParameterBuilder : IParameterBuilder
    {
        private readonly IEnumerable<IParameterBuilder> parameterBuilders;

        public CompositeParameterBuilder(IEnumerable<IParameterBuilder> parameterBuilders)
        {
            Contract.Requires(parameterBuilders != null);

            this.parameterBuilders = parameterBuilders;
        }

        public TypeReference ParameterType => parameterBuilders.Select(pb => pb.ParameterType).LastOrDefault();

        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            return parameterBuilders.SelectMany(pb => pb.Build(validateParameterDefinition));
        }
    }
}
