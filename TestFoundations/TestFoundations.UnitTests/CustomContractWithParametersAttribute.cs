using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts;

namespace TestFoundations.UnitTests
{
    public class CustomContractWithParametersAttribute : ContractAttribute
    {
        public CustomContractWithParametersAttribute(object x, object y, object z)
        {
            
        }
    }
}
