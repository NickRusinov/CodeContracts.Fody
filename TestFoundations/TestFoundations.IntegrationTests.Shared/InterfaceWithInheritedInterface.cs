using System;
using System.Collections.Generic;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public interface InterfaceWithInheritedInterfaceBase
    {
        [return: Range(-100, 100)]
        int MethodSumInt(int parameterLeft, int parameterRight);
    }
    
    public interface InterfaceWithInheritedInterface : InterfaceWithInheritedInterfaceBase
    {
        [return: Range(-100, 100)]
        long MethodSumLong(long parameterLeft, long parameterRight);
    }

    public class InterfaceWithInheritedInterfaceImplementation : InterfaceWithInheritedInterface
    {
        public int MethodSumInt(int parameterLeft, int parameterRight)
        {
            return parameterLeft + parameterRight;
        }

        public long MethodSumLong(long parameterLeft, long parameterRight)
        {
            return parameterLeft + parameterRight;
        }
    }
}
