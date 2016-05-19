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
    public class PropertyParameterParser : IMemberParameterParser
    {
        private readonly IMemberParameterParser memberParameterParser;

        public PropertyParameterParser(IMemberParameterParser memberParameterParser)
        {
            Contract.Requires(memberParameterParser != null);

            this.memberParameterParser = memberParameterParser;
        }

        public ParseResult Parse(TypeDefinition typeDefinition, string parameterString)
        {
            var parseResult = ParseResult.Empty;

            var propertyMatch = Regex.Match(parameterString, @"^([^\$\.][^\.]*)(?:\.(.+))?$", RegexOptions.Compiled);

            var property = typeDefinition.Properties.SingleOrDefault(pd => pd.Name == propertyMatch.Groups[1].Value);

            if (property != null && !string.IsNullOrEmpty(propertyMatch.Groups[1].Value))
                parseResult += new ParseResult(property.PropertyType.Resolve(), new PropertyParameterBuilder(property));

            if (property != null && !string.IsNullOrEmpty(propertyMatch.Groups[2].Value))
                parseResult += memberParameterParser.Parse(property.PropertyType.Resolve(), propertyMatch.Groups[2].Value);

            return parseResult;
        }
    }
}
