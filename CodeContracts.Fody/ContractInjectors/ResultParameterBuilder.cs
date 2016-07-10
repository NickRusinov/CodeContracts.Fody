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
    /// <summary>
    /// Creates il instructions for inject <see cref="Contract.Result{T}"/> method 
    /// to contract's validate method
    /// </summary>
    public class ResultParameterBuilder : IParameterBuilder
    {
        /// <summary>
        /// Definition of current weaving assembly
        /// </summary>
        private readonly ModuleDefinition moduleDefinition;
        
        /// <summary>
        /// Type of return value
        /// </summary>
        private readonly TypeReference typeReference;

        /// <summary>
        /// Initializes a new instance of class <see cref="ResultParameterBuilder"/>
        /// </summary>
        /// <param name="moduleDefinition">Definition of current weaving assembly</param>
        /// <param name="typeReference">Type of return value</param>
        public ResultParameterBuilder(ModuleDefinition moduleDefinition, TypeReference typeReference)
        {
            Contract.Requires(moduleDefinition != null);
            Contract.Requires(typeReference != null);

            this.moduleDefinition = moduleDefinition;
            this.typeReference = typeReference;
        }

        /// <inheritdoc/>
        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Call, ContractReferences.Result(moduleDefinition, typeReference));
        }
    }
}
