using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    public struct ParseResult
    {
        public static readonly ParseResult Empty = new ParseResult();

        public ParseResult(TypeDefinition parsedParameterType, IParameterBuilder parsedParameterBuilder)
        {
            Contract.Requires(parsedParameterType != null);
            Contract.Requires(parsedParameterBuilder != null);

            ParsedParameterType = parsedParameterType;
            ParsedParameterBuilder = parsedParameterBuilder;
        }

        public TypeDefinition ParsedParameterType { get; }

        public IParameterBuilder ParsedParameterBuilder { get; }

        public static ParseResult operator +(ParseResult left, ParseResult right)
        {
            if (Equals(left, Empty))
                return right;

            if (Equals(right, Empty))
                return left;

            return new ParseResult(right.ParsedParameterType, new CompositeParameterBuilder(left.ParsedParameterBuilder, right.ParsedParameterBuilder));
        }
    }
}
