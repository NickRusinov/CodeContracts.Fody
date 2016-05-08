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
    public class StringParameterBuilder : IParameterBuilder
    {
        private readonly string stringParameter;

        public StringParameterBuilder(string stringParameter)
        {
            Contract.Requires(stringParameter != null);

            this.stringParameter = stringParameter;
        }

        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Ldstr, stringParameter);
        }
    }
}
