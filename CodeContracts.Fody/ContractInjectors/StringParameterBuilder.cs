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
        private readonly TypeSystem typeSystem;

        private readonly string stringParameter;

        public StringParameterBuilder(TypeSystem typeSystem, string stringParameter)
        {
            Contract.Requires(typeSystem != null);
            Contract.Requires(stringParameter != null);

            this.typeSystem = typeSystem;
            this.stringParameter = stringParameter;
        }

        public TypeReference ParameterType => typeSystem.String;

        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Ldstr, stringParameter);
        }
    }
}
