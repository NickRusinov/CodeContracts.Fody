using System;
using System.Collections.Generic;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public interface InterfaceWithMethods
    {
        void MethodWithoutAll();

        void MethodWithPositiveParameter([Positive] short parameterWithPositive);

        void MethodWithPositiveAndNegativeParameters([Positive] short parameterWithPositive, [Negative] short parameterWithNegative);
    }

    public class InterfaceWithMethodsImplementation : InterfaceWithMethods
    {
        public void MethodWithoutAll()
        {

        }

        public void MethodWithPositiveParameter(short parameterWithPositive)
        {

        }

        public void MethodWithPositiveAndNegativeParameters(short parameterWithPositive, short parameterWithNegative)
        {

        }
    }
}
