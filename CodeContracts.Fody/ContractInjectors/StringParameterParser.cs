using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Parses a constant string which is not contract expression
    /// </summary>
    public class StringParameterParser : IMethodParameterParser
    {
        /// <inheritdoc/>
        public ParseResult Parse(MethodDefinition methodDefinition, string parameterString)
        {
            var parseResult = ParseResult.Empty;

            var matchSimpleString = Regex.Match(parameterString, @"^([^\$].*)$", RegexOptions.Compiled);
            if (matchSimpleString.Success)
                parseResult += new ParseResult(methodDefinition.Module.TypeSystem.String.Resolve(), new StringParameterBuilder(matchSimpleString.Groups[1].Value));

            var matchEscapedString = Regex.Match(parameterString, @"^\$(\$.+)$", RegexOptions.Compiled);
            if (matchEscapedString.Success)
                parseResult += new ParseResult(methodDefinition.Module.TypeSystem.String.Resolve(), new StringParameterBuilder(matchEscapedString.Groups[1].Value));

            return parseResult;
        }
    }
}
