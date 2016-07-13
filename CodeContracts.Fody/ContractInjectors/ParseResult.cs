using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractParameterBuilders;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractInjectors
{
    /// <summary>
    /// Represents results of parse parameter or property of contract expression
    /// </summary>
    public struct ParseResult
    {
        /// <summary>
        /// Represents failed results of parse parameter or property of contract expression
        /// </summary>
        public static readonly ParseResult Empty = new ParseResult();

        /// <summary>
        /// Initializes a new instance of struct <see cref="ParseResult"/>
        /// </summary>
        /// <param name="parsedParameterType">Result type of parsed contract expression</param>
        /// <param name="parsedParameterBuilder">Creates il instructions for parsed contract expression</param>
        public ParseResult(TypeDefinition parsedParameterType, IParameterBuilder parsedParameterBuilder)
        {
            Contract.Requires(parsedParameterType != null);
            Contract.Requires(parsedParameterBuilder != null);

            ParsedParameterType = parsedParameterType;
            ParsedParameterBuilder = parsedParameterBuilder;
        }

        /// <summary>
        /// Result type of parsed contract expression
        /// </summary>
        public TypeDefinition ParsedParameterType { get; }

        /// <summary>
        /// Creates il instructions for parsed contract expression
        /// </summary>
        public IParameterBuilder ParsedParameterBuilder { get; }

        /// <summary>
        /// Concatenate two results of parse parameter or property of contract expression
        /// </summary>
        /// <param name="left">First <see cref="ParseResult"/></param>
        /// <param name="right">Second <see cref="ParseResult"/></param>
        /// <returns>Concatenated <see cref="ParseResult"/></returns>
        /// <remarks>
        /// Result type of concatenated <see cref="ParseResult"/> is a result type of last non empty parameter
        /// </remarks>
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
