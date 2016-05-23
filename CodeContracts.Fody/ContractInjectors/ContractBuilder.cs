using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class ContractBuilder : IInstructionsBuilder
    {
        private readonly IContractMethodFactory contractMethodFactory;

        public ContractBuilder(IContractMethodFactory contractMethodFactory)
        {
            Contract.Requires(contractMethodFactory != null);

            this.contractMethodFactory = contractMethodFactory;
        }

        public IEnumerable<Instruction> Build(ContractValidate contractValidate)
        {
            return contractMethodFactory.Create(contractValidate.ValidateDefinition.ValidateMethod.Module, contractValidate.ValidateDefinition.ExceptionType, contractValidate.ValidateDefinition.ErrorMessage).Build(contractValidate);
        }
    }
}
