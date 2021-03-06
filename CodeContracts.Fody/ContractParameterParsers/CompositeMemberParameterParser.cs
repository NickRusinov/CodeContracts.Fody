﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractParameterParsers
{
    /// <summary>
    /// Parses contract expressions using some <see cref="IMemberParameterParser"/> till non empty result of parse
    /// </summary>
    public class CompositeMemberParameterParser : IMemberParameterParser
    {
        /// <summary>
        /// Collection of inner <see cref="IMemberParameterParser"/>
        /// </summary>
        private readonly IReadOnlyCollection<IMemberParameterParser> memberParameterParsers;

        /// <summary>
        /// Initializes a new instance of class <see cref="CompositeMemberParameterParser"/>
        /// </summary>
        /// <param name="memberParameterParsers">Collection of inner <see cref="IMemberParameterParser"/></param>
        public CompositeMemberParameterParser(IReadOnlyCollection<IMemberParameterParser> memberParameterParsers)
        {
            Contract.Requires(memberParameterParsers != null);

            this.memberParameterParsers = memberParameterParsers;
        }

        /// <summary>
        /// Initializes a new instance of class <see cref="CompositeMemberParameterParser"/>
        /// </summary>
        /// <param name="memberParameterParsers">Collection of inner <see cref="IMemberParameterParser"/></param>
        public CompositeMemberParameterParser(params IMemberParameterParser[] memberParameterParsers)
            : this(memberParameterParsers as IReadOnlyCollection<IMemberParameterParser>)
        {
            Contract.Requires(memberParameterParsers != null);
        }

        /// <inheritdoc/>
        public ParseResult Parse(TypeDefinition typeDefinition, string parameterString)
        {
            return memberParameterParsers.Select(mpp => mpp.Parse(typeDefinition, parameterString))
                .FirstOrDefault(pr => !Equals(pr, ParseResult.Empty));
        }
    }
}
