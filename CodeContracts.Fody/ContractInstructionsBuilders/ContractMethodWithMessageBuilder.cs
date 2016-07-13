using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractInjectors;
using CodeContracts.Fody.ContractValidateResolvers;
using CodeContracts.Fody.Internal;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeContracts.Fody.ContractInstructionsBuilders
{
    /// <summary>
    /// Creates il instructions for inject call specified method of <see cref="Contract"/> class 
    /// (example requries, ensures or invariant) with message overload
    /// </summary>
    public class ContractMethodWithMessageBuilder : IInstructionsBuilder
    {
        /// <summary>
        /// Creates il instructions for injected method of contract attribute
        /// </summary>
        private readonly IInstructionsBuilder instructionsBuilder;

        /// <summary>
        /// Method of <see cref="Contract"/> class with message overload
        /// </summary>
        private readonly MethodReference methodReference;

        /// <summary>
        /// String message constant for inject to <see cref="methodReference"/>'s parameter
        /// </summary>
        private readonly string message;

        /// <summary>
        /// Initializes a new instance of class <see cref="ContractMethodWithMessageBuilder"/>
        /// </summary>
        /// <param name="instructionsBuilder">Creates il instructions for injected method of contract attribute</param>
        /// <param name="methodReference">Method of <see cref="Contract"/> class with message overload</param>
        /// <param name="message">String message constant for inject to <paramref name="methodReference"/>'s parameter</param>
        public ContractMethodWithMessageBuilder(IInstructionsBuilder instructionsBuilder, MethodReference methodReference, string message)
        {
            Contract.Requires(instructionsBuilder != null);
            Contract.Requires(methodReference != null);
            Contract.Requires(message != null);

            this.instructionsBuilder = instructionsBuilder;
            this.methodReference = methodReference;
            this.message = message;
        }

        /// <inheritdoc/>
        public IEnumerable<Instruction> Build(ContractValidate contractValidate)
        {
            return EnumerableExtensions.Concat(
                Enumerable.Repeat(Instruction.Create(OpCodes.Nop), 1),
                instructionsBuilder.Build(contractValidate),
                Enumerable.Repeat(Instruction.Create(OpCodes.Ldstr, message), 1),
                Enumerable.Repeat(Instruction.Create(OpCodes.Call, methodReference), 1));
        }
    }
}
