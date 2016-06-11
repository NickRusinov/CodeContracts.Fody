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
    /// Represents contract invariant definition
    /// </summary>
    public class InvariantDefinition : ContractDefinition
    {
        /// <summary>
        /// Initializes a new instance of class <see cref="InvariantDefinition"/>
        /// </summary>
        /// <param name="contractAttribute">Custom attribute that contains information about contract</param>
        /// <param name="attributeProvider">Member to which was applied custom attribute</param>
        /// <param name="declaringType">Type that contains <paramref name="attributeProvider"/></param>
        public InvariantDefinition(CustomAttribute contractAttribute, ICustomAttributeProvider attributeProvider, TypeDefinition declaringType) 
            : base(contractAttribute, attributeProvider, declaringType)
        {
            Contract.Requires(contractAttribute != null);
            Contract.Requires(attributeProvider != null);
            Contract.Requires(declaringType != null);
        }
    }
}
