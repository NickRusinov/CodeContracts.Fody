using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts;

namespace TestFoundations.UnitTests
{
    public class CustomContractAttribute : ContractAttribute
    {
        public static bool ValidateMethodA()
        {
            throw new NotImplementedException();
        }
        
        public static bool ValidateMethodB()
        {
            throw new NotImplementedException();
        }
    }
}
