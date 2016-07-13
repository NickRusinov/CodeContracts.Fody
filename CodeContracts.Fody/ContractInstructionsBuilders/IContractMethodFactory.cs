using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInstructionsBuilders
{
    /// <summary>
    /// Creates a il instructions builder <see cref="IInstructionsBuilder"/> for injecting one 
    /// of <see cref="Contract"/>'s methods
    /// </summary>
    public interface IContractMethodFactory
    {
        /// <summary>
        /// Creates a il instructions builder for injecting one of <see cref="Contract"/>'s methods
        /// </summary>
        /// <param name="typeDefinition">Type of exception for requires</param>
        /// <param name="message">Error message string for requires, ensures or invariant</param>
        /// <returns>Builder for creating il instructions</returns>
        IInstructionsBuilder Create(TypeDefinition typeDefinition, string message);
    }
}
