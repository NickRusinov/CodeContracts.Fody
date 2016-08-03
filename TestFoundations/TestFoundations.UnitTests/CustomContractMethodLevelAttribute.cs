using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts;

namespace TestFoundations.UnitTests
{
    public class CustomContractMethodLevelAttribute : ContractAttribute
    {
        public static bool ValidateMethodA()
        {
            throw new NotImplementedException();
        }

        [ContractMessage("method level")]
        public static bool ValidateMethodB()
        {
            throw new NotImplementedException();
        }

        [ContractException(typeof(ArgumentNullException))]
        public static bool ValidateMethodC()
        {
            throw new NotImplementedException();
        }

        [ContractMessage("method level")]
        [ContractException(typeof(ArgumentNullException))]
        public static bool ValidateMethodD()
        {
            throw new NotImplementedException();
        }
    }
}
