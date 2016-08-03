using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeContracts;

namespace TestFoundations.UnitTests
{
    public class CustomContractWithMethodsAttribute : ContractAttribute
    {
        public static bool ValidateWithInt(int x, int y)
        {
            throw new NotImplementedException();
        }

        public static bool ValidateWithLong(long x, long y)
        {
            throw new NotImplementedException();
        }

        public static bool ValidateWithComparable(IComparable x, IComparable y)
        {
            throw new NotImplementedException();
        }

        public static bool ValidateWithEnumerable(IEnumerable x, IEnumerable y)
        {
            throw new NotImplementedException();
        }

        public static bool ValidateWithObject(object x, object y)
        {
            throw new NotImplementedException();
        }

        public static bool ValidateWithShortInt(short x, int y)
        {
            throw new NotImplementedException();
        }

        public static bool ValidateWithIntShort(int x, short y)
        {
            throw new NotImplementedException();
        }
    }
}
