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
    public class ThisParameterParser : IMethodParameterParser
    {
        private readonly IMemberParameterParser memberParameterParser;

        public ThisParameterParser(IMemberParameterParser memberParameterParser)
        {
            Contract.Requires(memberParameterParser != null);

            this.memberParameterParser = memberParameterParser;
        }

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
