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
    /// Creates il instructions for inject property to contract's validate method
    /// </summary>
    public class PropertyParameterBuilder : IParameterBuilder
    {
        /// <summary>
        /// Injected property
        /// </summary>
        private readonly PropertyDefinition propertyDefinition;

        /// <summary>
        /// Initializes a new instance of class <see cref="PropertyParameterBuilder"/>
        /// </summary>
        /// <param name="propertyDefinition">Injected property</param>
        public PropertyParameterBuilder(PropertyDefinition propertyDefinition)
        {
            Contract.Requires(propertyDefinition != null);
            Contract.Requires(propertyDefinition.GetMethod != null);

            this.propertyDefinition = propertyDefinition;
        }
        
        /// <inheridoc/>
        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            yield return Instruction.Create(OpCodes.Callvirt, propertyDefinition.GetMethod);
        }
    }
}
