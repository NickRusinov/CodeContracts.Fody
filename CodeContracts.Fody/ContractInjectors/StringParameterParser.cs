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
    public class StringParameterParser : IMethodParameterParser
    {
        public IEnumerable<IParameterBuilder> Parse(MethodDefinition methodDefinition, string parameterString)
        {
            var matchSimpleString = Regex.Match(parameterString, @"^([^\$].*)$", RegexOptions.Compiled);
            if (matchSimpleString.Success)
                yield return new StringParameterBuilder(methodDefinition.Module.TypeSystem, matchSimpleString.Groups[1].Value);

            var matchEscapedString = Regex.Match(parameterString, @"^\$(\$.+)$", RegexOptions.Compiled);
            if (matchEscapedString.Success)
                yield return new StringParameterBuilder(methodDefinition.Module.TypeSystem, matchEscapedString.Groups[1].Value);
        }
    }
}
