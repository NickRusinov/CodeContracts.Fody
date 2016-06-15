using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Parses a member access contract expression
    /// </summary>
    public interface IMemberParameterParser
    {
        /// <summary>
        /// Parses a low level (member access) contract expression associated with specified type
        /// </summary>
        /// <param name="typeDefinition">Type for which member access parses contract expression</param>
        /// <param name="parameterString">String repsentation of contract expression</param>
        /// <returns>Result of parse contract expression</returns>
        ParseResult Parse(TypeDefinition typeDefinition, string parameterString);
    }
}
