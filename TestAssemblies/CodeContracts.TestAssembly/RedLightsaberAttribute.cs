using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.TestAssembly
{
    public class RedLightsaberAttribute : ContractAttribute
    {
        public RedLightsaberAttribute(params object[] args)
        {

        }

        public static bool ValidateA(object self, object arg) => true;

        [ContractMessage("method level")]
        public static bool ValidateB(object self, bool arg) => true;

        [ContractException(typeof(ArgumentNullException))]
        public static bool ValidateC(object self, byte arg) => true;

        [ContractMessage("method level")]
        [ContractException(typeof(ArgumentNullException))]
        public static bool ValidateD(object self, short arg) => true;
    }
}
