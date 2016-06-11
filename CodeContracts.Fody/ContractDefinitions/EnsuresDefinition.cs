using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.ContractDefinitions
{
    /// <summary>
    /// Represents contract ensures definition
    /// </summary>
    public class EnsuresDefinition : ContractDefinition
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="EnsuresDefinition"/>
        /// </summary>
        /// <param name="contractAttribute">Custom attribute that contains information about contract</param>
        /// <param name="attributeProvider">Member to which was applied custom attribute</param>
        /// <param name="contractMethod">Method that references to custom contract attribute</param>
        public EnsuresDefinition(CustomAttribute contractAttribute, ICustomAttributeProvider attributeProvider, MethodDefinition contractMethod)
            : base(contractAttribute, attributeProvider, contractMethod.DeclaringType)
        {
            Contract.Requires(contractAttribute != null);
            Contract.Requires(attributeProvider != null);
            Contract.Requires(contractMethod != null);

            ContractMethod = contractMethod;
        }

        /// <summary>
        /// Method that references to custom contract attribute
        /// </summary>
        public MethodDefinition ContractMethod { get; set; }
    }
}
