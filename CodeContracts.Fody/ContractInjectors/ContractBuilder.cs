using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Creates il instructions for injecting one of <see cref="Contract"/>'s methods
    /// </summary>
    public class ContractBuilder : IInstructionsBuilder
    {
        /// <summary>
        /// Creates a il instructions builder <see cref="IInstructionsBuilder"/> for injecting one 
        /// of <see cref="Contract"/>'s methods
        /// </summary>
        private readonly IContractMethodFactory contractMethodFactory;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractBuilder"/>
        /// </summary>
        /// <param name="contractMethodFactory">
        /// Creates a il instructions builder <see cref="IInstructionsBuilder"/> for injecting one 
        /// of <see cref="Contract"/>'s methods
        /// </param>
        public ContractBuilder(IContractMethodFactory contractMethodFactory)
        {
            Contract.Requires(contractMethodFactory != null);

            this.contractMethodFactory = contractMethodFactory;
        }

        /// <inheritdoc/>
        public IEnumerable<Instruction> Build(ContractValidate contractValidate)
        {
            return contractMethodFactory.Create(contractValidate.ValidateDefinition.ExceptionType, contractValidate.ValidateDefinition.ErrorMessage).Build(contractValidate);
        }
    }
}
