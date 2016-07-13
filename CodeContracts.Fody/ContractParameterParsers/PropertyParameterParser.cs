using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractParameterBuilders;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractParameterParsers
{
    /// <summary>
    /// Parses a access to property of some type of contract expression
    /// </summary>
    public class PropertyParameterParser : IMemberParameterParser
    {
        /// <summary>
        /// Parses a member access contract expression
        /// </summary>
        private readonly IMemberParameterParser memberParameterParser;

        /// <summary>
        /// Initializes a new instance of class <see cref="PropertyParameterParser"/>
        /// </summary>
        /// <param name="memberParameterParser">Parses a member access contract expression</param>
        public PropertyParameterParser(IMemberParameterParser memberParameterParser)
        {
            Contract.Requires(memberParameterParser != null);

            this.memberParameterParser = memberParameterParser;
        }

        /// <inheritdoc/>
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
