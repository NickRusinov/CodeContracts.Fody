using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts.Fody.ContractDefinitions;
using CodeContracts.Fody.Internal;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractScanners
{
    /// <summary>
    /// Scans custom contract attributes in a method
    /// </summary>
    public class MethodScanner : IMethodScanner
    {
        /// <summary>
        /// Scans custom contract attributes in a parameter of method
        /// </summary>
        private readonly IParameterScanner parameterScanner;

        /// <summary>
        /// Scans custom contract attributes in a method return value
        /// </summary>
        private readonly IMethodReturnScanner methodReturnScanner;

        /// <summary>
        /// Criteria that define that custom attribute is contract attribute
        /// </summary>
        private readonly IContractCriteria contractCriteria;

        /// <summary>
        /// Initializes a new instance of class <see cref="MethodScanner"/>
        /// </summary>
        /// <param name="parameterScanner">Scans custom contract attributes in a parameter of method</param>
        /// <param name="methodReturnScanner">Scans custom contract attributes in a method return value</param>
        /// <param name="contractCriteria">Criteria that define that custom attribute is contract attribute</param>
        public MethodScanner(IParameterScanner parameterScanner, IMethodReturnScanner methodReturnScanner, IContractCriteria contractCriteria)
        {
            Contract.Requires(parameterScanner != null);
            Contract.Requires(methodReturnScanner != null);
            Contract.Requires(contractCriteria != null);

            this.parameterScanner = parameterScanner;
            this.methodReturnScanner = methodReturnScanner;
            this.contractCriteria = contractCriteria;
        }

        /// <inheritdoc/>
        public IEnumerable<ContractDefinition> Scan(MethodDefinition methodDefinition)
        {
            return EnumerableExtensions.Concat(
                from parameterDefinition in methodDefinition.Parameters
                from contractDefinition in parameterScanner.Scan(parameterDefinition)
                select contractDefinition,

                from methodReturnDefinition in Enumerable.Repeat(methodDefinition.MethodReturnType, 1)
                from contractDefinition in methodReturnScanner.Scan(methodReturnDefinition)
                select contractDefinition,
                
                from contractAttribute in methodDefinition.CustomAttributes
                where contractCriteria.IsContract(contractAttribute)
                select new RequiresDefinition(contractAttribute, methodDefinition, methodDefinition));
        }
    }
}
