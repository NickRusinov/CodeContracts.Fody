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
    public class FieldParameterParser : IMemberParameterParser
    {
        private readonly IMemberParameterParser memberParameterParser;

        public FieldParameterParser(IMemberParameterParser memberParameterParser)
        {
            Contract.Requires(memberParameterParser != null);

            this.memberParameterParser = memberParameterParser;
        }

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
