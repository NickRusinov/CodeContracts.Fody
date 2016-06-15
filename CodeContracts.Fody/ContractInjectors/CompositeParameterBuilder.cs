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
    /// Creates il instructions for inject parameter to contract's validate method using some 
    /// <see cref="IParameterBuilder"/> in series
    /// </summary>
    public class CompositeParameterBuilder : IParameterBuilder
    {
        /// <summary>
        /// Collection of inner <see cref="IParameterBuilder"/>
        /// </summary>
        private readonly IReadOnlyCollection<IParameterBuilder> parameterBuilders;

        /// <summary>
        /// Initializes a new instance of class <see cref="CompositeParameterBuilder"/>
        /// </summary>
        /// <param name="parameterBuilders">Collection of inner <see cref="IParameterBuilder"/></param>
        public CompositeParameterBuilder(IReadOnlyCollection<IParameterBuilder> parameterBuilders)
        {
            Contract.Requires(parameterBuilders != null);

            this.parameterBuilders = parameterBuilders;
        }

        /// <summary>
        /// Initializes a new instance of class <see cref="CompositeParameterBuilder"/>
        /// </summary>
        /// <param name="parameterBuilders">Collection of inner <see cref="IParameterBuilder"/></param>
        public CompositeParameterBuilder(params IParameterBuilder[] parameterBuilders)
            : this(parameterBuilders as IReadOnlyCollection<IParameterBuilder>)
        {
            Contract.Requires(parameterBuilders != null);
        }
        
        /// <inheritdoc/>
        public IEnumerable<Instruction> Build(ParameterDefinition validateParameterDefinition)
        {
            return parameterBuilders.SelectMany(pb => pb.Build(validateParameterDefinition));
        }
    }
}
