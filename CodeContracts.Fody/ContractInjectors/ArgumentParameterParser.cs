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
    public class ArgumentParameterParser : IMethodParameterParser
    {
        private readonly IMemberParameterParser memberParameterParser;

        public ArgumentParameterParser(IMemberParameterParser memberParameterParser)
        {
            Contract.Requires(memberParameterParser != null);

            this.memberParameterParser = memberParameterParser;
        }

        public ParseResult Parse(MethodDefinition methodDefinition, string parameterString)
        {
            var parseResult = ParseResult.Empty;

            var argumentMatch = Regex.Match(parameterString, @"^\$([^\$\.][^\.]*)(?:\.(.+))?$", RegexOptions.Compiled);

            var argument = methodDefinition.Parameters.SingleOrDefault(pd => pd.Name == argumentMatch.Groups[1].Value);

            if (argument != null && !string.IsNullOrEmpty(argumentMatch.Groups[1].Value))
                parseResult += new ParseResult(argument.ParameterType.Resolve(), new ArgumentParameterBuilder(argument));

            if (argument != null && !string.IsNullOrEmpty(argumentMatch.Groups[2].Value))
                parseResult += memberParameterParser.Parse(argument.ParameterType.Resolve(), argumentMatch.Groups[2].Value);

            return parseResult;
        }
    }
}
