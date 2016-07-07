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
    /// Creates il instructions for inject boxing conversion from value type to reference type 
    /// to contract's validate method
    /// </summary>
    public class BoxParameterBuilder : IParameterBuilder
    {
        /// <summary>
        /// Definition of current weaving assembly
        /// </summary>
        private readonly ModuleDefinition moduleDefinition;

        /// <summary>
        /// Original type from which will be boxing conversion
        /// </summary>
        private readonly TypeReference originalTypeReference;

        /// <summary>
        /// Initializes a new instance of class <see cref="BoxParameterBuilder"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <param name="originalTypeReference">Original type from which will be boxing conversion</param>
        public BoxParameterBuilder(ModuleDefinition moduleDefinition, TypeReference originalTypeReference)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(originalTypeReference != null);

            this.moduleDefinition = moduleDefinition;
            this.originalTypeReference = originalTypeReference;
        }

        /// <inheritdoc/>
        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            if (originalTypeReference.Resolve().IsValueType && !validateParameterDefinition.ParameterType.Resolve().IsValueType)
                yield return Instruction.Create(OpCodes.Box, moduleDefinition.ImportReference(originalTypeReference));
        }
    }
}
