using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public class CompositeMemberParameterParser : IMemberParameterParser
    {
        private readonly IEnumerable<IMemberParameterParser> memberParameterParsers;

        public CompositeMemberParameterParser(IEnumerable<IMemberParameterParser> memberParameterParsers)
        {
            Contract.Requires(memberParameterParsers != null);

            this.memberParameterParsers = memberParameterParsers;
        }

        public CompositeMemberParameterParser(params IMemberParameterParser[] memberParameterParsers)
            : this(memberParameterParsers as IEnumerable<IMemberParameterParser>)
        {
            Contract.Requires(memberParameterParsers != null);
        }

        public ParseResult Parse(TypeDefinition typeDefinition, string parameterString)
        {
            return memberParameterParsers.Select(mpp => mpp.Parse(typeDefinition, parameterString))
                .FirstOrDefault(pr => !Equals(pr, ParseResult.Empty));
        }
    }
}
