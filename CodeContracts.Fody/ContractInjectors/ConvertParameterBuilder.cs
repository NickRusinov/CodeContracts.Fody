using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ConvertParameterBuilder : IParameterBuilder
    {
        private readonly ModuleDefinition moduleDefinition;

        public ConvertParameterBuilder(ModuleDefinition moduleDefinition)
        {
            Contract.Requires(moduleDefinition != null);

            this.moduleDefinition = moduleDefinition;
        }

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
