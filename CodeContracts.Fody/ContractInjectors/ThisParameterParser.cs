using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractParameterBuilders;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Parses a access to "this" reference contract expression
    /// </summary>
    public class ThisParameterParser : IMethodParameterParser
    {
        /// <summary>
        /// Parses a member access contract expression
        /// </summary>
        private readonly IMemberParameterParser memberParameterParser;

        /// <summary>
        /// Initializes a new instance of class <see cref="ThisParameterParser"/>
        /// </summary>
        /// <param name="memberParameterParser">Parses a member access contract expression</param>
        public ThisParameterParser(IMemberParameterParser memberParameterParser)
        {
            Contract.Requires(memberParameterParser != null);

            this.memberParameterParser = memberParameterParser;
        }

        /// <inheritdoc/>
        public ParseResult Parse(MethodDefinition methodDefinition, string parameterString)
        {
            var parseResult = ParseResult.Empty;

            var thisMatch = Regex.Match(parameterString, @"^\$(?:\.(.+))?$", RegexOptions.Compiled);

            if (thisMatch.Success)
                parseResult += new ParseResult(methodDefinition.DeclaringType, new ThisParameterBuilder());

            if (!string.IsNullOrEmpty(thisMatch.Groups[1].Value))
                parseResult += memberParameterParser.Parse(methodDefinition.DeclaringType, thisMatch.Groups[1].Value);

            return parseResult;
        }
    }
}
