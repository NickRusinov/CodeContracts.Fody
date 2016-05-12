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

        public IEnumerable<IParameterBuilder> Parse(MethodDefinition methodDefinition, string parameterString)
        {
            var thisMatch = Regex.Match(parameterString, @"^\$(?:\.(.+))?$", RegexOptions.Compiled);
            if (thisMatch.Success)
                yield return new ThisParameterBuilder(methodDefinition.DeclaringType);

            if (!string.IsNullOrEmpty(thisMatch.Groups[1].Value))
                foreach (var parameterBuilder in memberParameterParser.Parse(methodDefinition.DeclaringType, thisMatch.Groups[1].Value))
                    yield return parameterBuilder;
        }
    }
}
