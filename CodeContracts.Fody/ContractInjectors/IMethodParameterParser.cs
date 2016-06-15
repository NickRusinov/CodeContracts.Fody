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
    /// Parses a top level contract expression
    /// </summary>
    public interface IMethodParameterParser
    {
        /// <summary>
        /// Parses a top level contract expression associated with specified method
        /// </summary>
        /// <param name="methodDefinition">Method in which will be injected contract expression</param>
        /// <param name="parameterString">String repsentation of contract expression</param>
        /// <returns>Result of parse contract expression</returns>
        ParseResult Parse(MethodDefinition methodDefinition, string parameterString);
    }
}
