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

        public IEnumerable<IParameterBuilder> Parse(TypeDefinition typeDefinition, string parameterString)
        {
            var propertyMatch = Regex.Match(parameterString, @"^([^\$\.][^\.]*)(?:\.(.+))?$", RegexOptions.Compiled);
            if (!propertyMatch.Success)
                yield break;

            var property = typeDefinition.Properties.SingleOrDefault(pd => pd.Name == propertyMatch.Groups[1].Value);
            if (property != null)
                yield return new PropertyParameterBuilder(property);

            if (property != null && !string.IsNullOrEmpty(propertyMatch.Groups[2].Value))
                foreach (var parameterBuilder in memberParameterParser.Parse(property.PropertyType.Resolve(), propertyMatch.Groups[2].Value))
                    yield return parameterBuilder;
        }
    }
}
