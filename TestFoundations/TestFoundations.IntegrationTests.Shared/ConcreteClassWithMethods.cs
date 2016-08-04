using System;
using System.Collections.Generic;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public class ConcreteClassWithMethods
    {
        public void MethodWithoutAll()
        {

        }

        public void MethodWithNotNullParameter([NotNull] object parameter)
        {

        }

        public void MethodWithTrueAndFalseParameters([True] bool parameterA, [False] bool parameterB)
        {
            
        }
    }
}
