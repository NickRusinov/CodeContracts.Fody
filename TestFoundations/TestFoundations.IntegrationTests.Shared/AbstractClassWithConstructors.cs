using System;
using System.Collections.Generic;
using System.Text;
using CodeContracts;

namespace TestFoundations.IntegrationTests.Shared
{
    public abstract class AbstractClassWithConstructors
    {
        protected AbstractClassWithConstructors()
        {
            
        }

        protected AbstractClassWithConstructors([Is(typeof(int))] object parameterWithIsInt)
        {
            
        }
    }

    public class AbstractClassWithConstructorsImplementation : AbstractClassWithConstructors
    {
        public AbstractClassWithConstructorsImplementation()
            : base()
        {
            
        }

        public AbstractClassWithConstructorsImplementation(object parameterWithIsInt) 
            : base(parameterWithIsInt)
        {

        }
    }
}
