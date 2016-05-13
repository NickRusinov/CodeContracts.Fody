using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.MethodBodyResolvers
{
    public class InvariantMethodResolver : IInvariantMethodResolver
    {
        private readonly IInvariantMethodBuilder invariantMethodBuilder;

        public InvariantMethodResolver(IInvariantMethodBuilder invariantMethodBuilder)
        {
            Contract.Requires(invariantMethodBuilder != null);

            this.invariantMethodBuilder = invariantMethodBuilder;
        }

        public MethodDefinition Resolve(TypeDefinition typeDefinition)
        {
            var contractInvariantMethod = ResolveInvariantMethod(typeDefinition);

            if (contractInvariantMethod == null)
                return invariantMethodBuilder.Build(typeDefinition);

            return contractInvariantMethod;
        }

        protected MethodDefinition ResolveInvariantMethod(TypeDefinition typeDefinition)
        {
            Contract.Requires(typeDefinition != null);

            var contractInvariantMethodDefinition = typeDefinition.Module.ImportReference(typeof(ContractInvariantMethodAttribute)).Resolve();
            var contractInvariantMethod = typeDefinition.Methods.SingleOrDefault(md => md.CustomAttributes.Any(ca => Equals(ca.AttributeType.Resolve(), contractInvariantMethodDefinition)));

            return contractInvariantMethod;
        }
    }
}
