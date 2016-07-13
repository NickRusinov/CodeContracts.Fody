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
    /// Parses a access to field of some type of contract expression
    /// </summary>
    public class FieldParameterParser : IMemberParameterParser
    {
        /// <summary>
        /// Parses a member access contract expression
        /// </summary>
        private readonly IMemberParameterParser memberParameterParser;

        /// <summary>
        /// Initializes a new instance of class <see cref="FieldParameterParser"/>
        /// </summary>
        /// <param name="memberParameterParser">Parses a member access contract expression</param>
        public FieldParameterParser(IMemberParameterParser memberParameterParser)
        {
            Contract.Requires(memberParameterParser != null);

            this.memberParameterParser = memberParameterParser;
        }

        /// <inheritdoc/>
        public ParseResult Parse(TypeDefinition typeDefinition, string parameterString)
        {
            var parseResult = ParseResult.Empty;

            var fieldMatch = Regex.Match(parameterString, @"^([^\$\.][^\.]*)(?:\.(.+))?$", RegexOptions.Compiled);
            
            var field = typeDefinition.Fields.SingleOrDefault(fd => fd.Name == fieldMatch.Groups[1].Value);

            if (field != null && !string.IsNullOrEmpty(fieldMatch.Groups[1].Value))
                parseResult += new ParseResult(field.FieldType.Resolve(), new FieldParameterBuilder(field));

            if (field != null && !string.IsNullOrEmpty(fieldMatch.Groups[2].Value))
                parseResult += memberParameterParser.Parse(field.FieldType.Resolve(), fieldMatch.Groups[2].Value);

            return parseResult;
        }
    }
}
