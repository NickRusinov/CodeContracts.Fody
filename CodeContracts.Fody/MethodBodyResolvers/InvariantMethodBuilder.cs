using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.MethodBodyResolvers
{
    public class InvariantMethodBuilder : IInvariantMethodBuilder
    {
        public MethodDefinition Build(TypeDefinition typeDefinition)
        {
            // todo - алгоритм генерации метода инварианта
            return typeDefinition.Methods.First();
        }
    }
}
