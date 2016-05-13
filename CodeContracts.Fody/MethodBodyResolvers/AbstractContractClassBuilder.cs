using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace CodeContracts.Fody.MethodBodyResolvers
{
    public class AbstractContractClassBuilder : IAbstractContractClassBuilder
    {
        public TypeDefinition Build(TypeDefinition typeDefinition)
        {
            // todo - алгоритм генерации контрактного класса для абстрактного класса
            return typeDefinition;
        }
    }
}
