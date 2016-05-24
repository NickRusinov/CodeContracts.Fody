using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class CompositeMethodParameterParser : IMethodParameterParser
    {
        private readonly IEnumerable<IMethodParameterParser> methodParameterParsers;

        public CompositeMethodParameterParser(IEnumerable<IMethodParameterParser> methodParameterParsers)
        {
            Contract.Requires(methodParameterParsers != null);

            this.methodParameterParsers = methodParameterParsers;
        }

        public CompositeMethodParameterParser(params IMethodParameterParser[] methodParameterParsers)
            : this(methodParameterParsers as IEnumerable<IMethodParameterParser>)
        {
            Contract.Requires(methodParameterParsers != null);
        }

        public ParseResult Parse(MethodDefinition methodDefinition, string parameterString)
        {
            return methodParameterParsers.Select(mpp => mpp.Parse(methodDefinition, parameterString))
                .FirstOrDefault(pr => !Equals(pr, ParseResult.Empty));
        }
    }
}
