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

        public IEnumerable<IParameterBuilder> Parse(MethodDefinition methodDefinition, string parameterString)
        {
            var argumentMatch = Regex.Match(parameterString, @"^\$([^\$\.][^\.]*)(?:\.(.+))?$", RegexOptions.Compiled);
            if (!argumentMatch.Success)
                yield break;
            
            var argument = methodDefinition.Parameters.SingleOrDefault(pd => pd.Name == argumentMatch.Groups[1].Value);
            if (argument != null)
                yield return new ArgumentParameterBuilder(argument);

            if (argument != null && !string.IsNullOrEmpty(argumentMatch.Groups[2].Value))
                foreach (var parameterBuilder in memberParameterParser.Parse(argument.ParameterType.Resolve(), argumentMatch.Groups[2].Value))
                    yield return parameterBuilder;
        }
    }
}
