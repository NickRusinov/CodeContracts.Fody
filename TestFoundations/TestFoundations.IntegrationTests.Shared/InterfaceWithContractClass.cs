using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    [ContractClass(typeof(InterfaceWithContractClassContracts))]
    public interface InterfaceWithContractClass
    {
        InterfaceWithContractClass MethodWithContractClass();

        [return: This]
        InterfaceWithContractClass MethodWithContractAttribute();
    }

    [ContractClassFor(typeof(InterfaceWithContractClass))]
    internal abstract class InterfaceWithContractClassContracts : InterfaceWithContractClass
    {
        public InterfaceWithContractClass MethodWithContractClass()
        {
            Contract.Ensures(ReferenceEquals(Contract.Result<InterfaceWithContractClass>(), this));

            throw new NotImplementedException();
        }

        public InterfaceWithContractClass MethodWithContractAttribute()
        {
            throw new NotImplementedException();
        }
    }

    public class InterfaceWithContractClassImplementation : InterfaceWithContractClass
    {
        public InterfaceWithContractClass FieldWithContractClassReturnValue;
        public InterfaceWithContractClass MethodWithContractClass()
        {
            return FieldWithContractClassReturnValue;
        }

        public InterfaceWithContractClass FieldWithContractAttributeReturnValue;
        public InterfaceWithContractClass MethodWithContractAttribute()
        {
            return FieldWithContractAttributeReturnValue;
        }
    }
}
