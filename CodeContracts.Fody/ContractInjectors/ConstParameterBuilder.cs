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
    public class ConstParameterBuilder : IParameterBuilder
    {
        private readonly object constParameter;

        public ConstParameterBuilder(object constParameter)
        {
            Contract.Requires(constParameter != null);

            this.constParameter = constParameter;
        }

        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            // todo - алгоритм создания инструкций для постоянного значения
            throw new NotImplementedException();
        }
    }
}
