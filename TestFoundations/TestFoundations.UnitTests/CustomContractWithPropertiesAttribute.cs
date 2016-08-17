using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts;

namespace TestFoundations.UnitTests
{
    public class CustomContractWithPropertiesAttribute : ContractAttribute
    {
        public object X { get; set; }

        public object Y { get; set; }

        public object Z { get; set; }
    }
}
