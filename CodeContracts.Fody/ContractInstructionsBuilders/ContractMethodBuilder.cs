using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInstructionsBuilders
{
    /// <summary>
    /// Creates il instructions for inject call specified method of <see cref="Contract"/> class 
    /// (example requries, ensures or invariant) without message overload
    /// </summary>
    public class ContractMethodBuilder : IInstructionsBuilder
    {
        /// <summary>
        /// Creates il instructions for injected method of contract attribute
        /// </summary>
        private readonly IInstructionsBuilder instructionsBuilder;

        /// <summary>
        /// Method of <see cref="Contract"/> class without message overload
        /// </summary>
        private readonly MethodReference methodReference;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractMethodBuilder"/>
        /// </summary>
        /// <param name="instructionsBuilder"> Creates il instructions for injected method of contract attribute</param>
        /// <param name="methodReference">Method of <see cref="Contract"/> class without message overload</param>
        public ContractMethodBuilder(IInstructionsBuilder instructionsBuilder, MethodReference methodReference)
        {
            Contract.Requires(methodReference != null);
            Contract.Requires(instructionsBuilder != null);

            this.instructionsBuilder = instructionsBuilder;
            this.methodReference = methodReference;
        }

        /// <inheridoc/>
        public IEnumerable<Instruction> Build(ContractValidate contractValidate)
        {
            return EnumerableExtensions.Concat(
                Enumerable.Repeat(Instruction.Create(OpCodes.Nop), 1),
                instructionsBuilder.Build(contractValidate),
                Enumerable.Repeat(Instruction.Create(OpCodes.Call, methodReference), 1));
        }
    }
}
