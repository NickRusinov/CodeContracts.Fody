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
    /// <summary>
    /// Creates il instructions for inject constant value to contract's validate method
    /// </summary>
    public class ConstParameterBuilder : IParameterBuilder
    {
        /// <summary>
        /// Injected constant value
        /// </summary>
        private readonly object constParameter;

        /// <summary>
        /// Initializes a new instance of class <see cref="ConstParameterBuilder"/>
        /// </summary>
        /// <param name="constParameter">Injected constant value</param>
        public ConstParameterBuilder(object constParameter)
        {
            this.constParameter = constParameter;
        }

        /// <inheritdoc/>
        /// <exception cref="NotSupportedException">Type of injected constant value is not supported</exception>
        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            if (constParameter == null)
                yield return Instruction.Create(OpCodes.Ldnull);

            else if (constParameter is sbyte)
                yield return Instruction.Create(OpCodes.Ldc_I4, unchecked((int)(sbyte)constParameter));

            else if (constParameter is byte)
                yield return Instruction.Create(OpCodes.Ldc_I4, unchecked((int)(byte)constParameter));

            else if (constParameter is short)
                yield return Instruction.Create(OpCodes.Ldc_I4, unchecked((int)(short)constParameter));

            else if (constParameter is ushort)
                yield return Instruction.Create(OpCodes.Ldc_I4, unchecked((int)(ushort)constParameter));

            else if (constParameter is int)
                yield return Instruction.Create(OpCodes.Ldc_I4, unchecked((int)(int)constParameter));

            else if (constParameter is uint)
                yield return Instruction.Create(OpCodes.Ldc_I4, unchecked((int)(uint)constParameter));

            else if (constParameter is long)
                yield return Instruction.Create(OpCodes.Ldc_I8, unchecked((long)(long)constParameter));

            else if (constParameter is ulong)
                yield return Instruction.Create(OpCodes.Ldc_I8, unchecked((long)(ulong)constParameter));

            else if (constParameter is float)
                yield return Instruction.Create(OpCodes.Ldc_R4, (float)constParameter);

            else if (constParameter is double)
                yield return Instruction.Create(OpCodes.Ldc_R8, (double)constParameter);

            else
                throw new NotSupportedException();
        }
    }
}
