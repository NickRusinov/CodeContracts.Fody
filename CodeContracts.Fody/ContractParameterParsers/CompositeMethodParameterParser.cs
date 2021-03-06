﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractParameterParsers
{
    /// <summary>
    /// Parses contract expressions using some <see cref="IMethodParameterParser"/> till non empty result of parse
    /// </summary>
    public class CompositeMethodParameterParser : IMethodParameterParser
    {
        /// <summary>
        /// Collection of inner <see cref="IMethodParameterParser"/>
        /// </summary>
        private readonly IReadOnlyCollection<IMethodParameterParser> methodParameterParsers;

        /// <summary>
        /// Initializes a new instance of class <see cref="CompositeMethodParameterParser"/>
        /// </summary>
        /// <param name="methodParameterParsers">Collection of inner <see cref="IMethodParameterParser"/></param>
        public CompositeMethodParameterParser(IReadOnlyCollection<IMethodParameterParser> methodParameterParsers)
        {
            Contract.Requires(methodParameterParsers != null);

            this.methodParameterParsers = methodParameterParsers;
        }

        /// <summary>
        /// Initializes a new instance of class <see cref="CompositeMethodParameterParser"/>
        /// </summary>
        /// <param name="methodParameterParsers">Collection of inner <see cref="IMethodParameterParser"/></param>
        public CompositeMethodParameterParser(params IMethodParameterParser[] methodParameterParsers)
            : this(methodParameterParsers as IReadOnlyCollection<IMethodParameterParser>)
        {
            Contract.Requires(methodParameterParsers != null);
        }

        /// <inheritdoc/>
        public ParseResult Parse(MethodDefinition methodDefinition, string parameterString)
        {
            return methodParameterParsers.Select(mpp => mpp.Parse(methodDefinition, parameterString))
                .FirstOrDefault(pr => !Equals(pr, ParseResult.Empty));
        }
    }
}
