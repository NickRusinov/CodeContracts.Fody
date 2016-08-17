using System;
using System.Collections.Generic;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public abstract class AbstractClassWithMethods
    {
        public void MethodWithoutAll()
        {
            
        }

        public void MethodWithNullParameter([Null] object parameterWithNull)
        {
            
        }

        public void MethodWithNullAndNotNullParameters([Null] object parameterWithNull, [NotNull] object parameterWithNotNull)
        {
            
        }

        public abstract void MethodAbstractWithoutAll();

        public abstract void MethodAbstractWithNullParameter([Null] object parameterWithNull);

        public abstract void MethodAbstractWithNullAndNotNullParameters([Null] object parameterWithNull, [NotNull] object parameterWithNotNull);
    }

    public class AbstractClassWithMethodsImplementation : AbstractClassWithMethods
    {
        public override void MethodAbstractWithoutAll()
        {

        }

        public override void MethodAbstractWithNullParameter(object parameterWithNull)
        {

        }

        public override void MethodAbstractWithNullAndNotNullParameters(object parameterWithNull, object parameterWithNotNull)
        {

        }
    }
}
