using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.RegularExpressions;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    [ContractClass(typeof(AbstractClassWithContractClassContracts))]
    public abstract class AbstractClassWithContractClass
    {
        public abstract void MethodAbstractWithContractClass(string parameterWithRegexD);

        public abstract void MethodAbstractWithContractAttribute([Regex(@"\d")] string parameterWithRegexD);
    }

    [ContractClassFor(typeof(AbstractClassWithContractClass))]
    internal abstract class AbstractClassWithContractClassContracts : AbstractClassWithContractClass
    {
        public override void MethodAbstractWithContractClass(string parameterWithRegexD)
        {
            Contract.Requires(Regex.IsMatch(parameterWithRegexD, @"\d"));

            throw new NotImplementedException();
        }

        public override void MethodAbstractWithContractAttribute(string parameterWithRegexD)
        {
            throw new NotImplementedException();
        }
    }

    public class AbstractClassWithContratcClassImplementation : AbstractClassWithContractClass
    {
        public override void MethodAbstractWithContractClass(string parameterWithRegexD)
        {

        }

        public override void MethodAbstractWithContractAttribute(string parameterWithRegexD)
        {

        }
    }
}
