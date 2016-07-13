using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractParameterBuilders
{
    /// <summary>
    /// Creates il instructions for inject convertion operation to contract's validate method
    /// </summary>
    public class ConvertParameterBuilder : IParameterBuilder
    {
        /// <summary>
        /// Definition of current weaving assembly
        /// </summary>
        private readonly ModuleDefinition moduleDefinition;

        /// <summary>
        /// Initializes a new instance of class <see cref="ConvertParameterBuilder"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        public ConvertParameterBuilder(ModuleDefinition moduleDefinition)
        {
            Contract.Requires(moduleDefinition != null);

            this.moduleDefinition = moduleDefinition;
        }

        /// <inheritdoc/>
        /// <exception cref="NotSupportedException">
        /// Type of parameter of contract's validate method is not supported for conversion
        /// </exception>
        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            if (TypeReferenceComparer.Instance.Equals(validateParameterDefinition.ParameterType, moduleDefinition.TypeSystem.SByte))
                yield return Instruction.Create(OpCodes.Conv_I1);

            else if (TypeReferenceComparer.Instance.Equals(validateParameterDefinition.ParameterType, moduleDefinition.TypeSystem.Byte))
                yield return Instruction.Create(OpCodes.Conv_U1);

            else if (TypeReferenceComparer.Instance.Equals(validateParameterDefinition.ParameterType, moduleDefinition.TypeSystem.Int16))
                yield return Instruction.Create(OpCodes.Conv_I2);

            else if (TypeReferenceComparer.Instance.Equals(validateParameterDefinition.ParameterType, moduleDefinition.TypeSystem.UInt16))
                yield return Instruction.Create(OpCodes.Conv_U2);

            else if (TypeReferenceComparer.Instance.Equals(validateParameterDefinition.ParameterType, moduleDefinition.TypeSystem.Int32))
                yield return Instruction.Create(OpCodes.Conv_I4);

            else if (TypeReferenceComparer.Instance.Equals(validateParameterDefinition.ParameterType, moduleDefinition.TypeSystem.UInt32))
                yield return Instruction.Create(OpCodes.Conv_U4);

            else if (TypeReferenceComparer.Instance.Equals(validateParameterDefinition.ParameterType, moduleDefinition.TypeSystem.Int64))
                yield return Instruction.Create(OpCodes.Conv_I8);

            else if (TypeReferenceComparer.Instance.Equals(validateParameterDefinition.ParameterType, moduleDefinition.TypeSystem.UInt64))
                yield return Instruction.Create(OpCodes.Conv_U8);

            else if (TypeReferenceComparer.Instance.Equals(validateParameterDefinition.ParameterType, moduleDefinition.TypeSystem.Single))
                yield return Instruction.Create(OpCodes.Conv_R4);

            else if (TypeReferenceComparer.Instance.Equals(validateParameterDefinition.ParameterType, moduleDefinition.TypeSystem.Double))
                yield return Instruction.Create(OpCodes.Conv_R8);

            else
                throw new NotSupportedException();
        }
    }
}
