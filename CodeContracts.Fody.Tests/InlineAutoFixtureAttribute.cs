using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture.Xunit2;

namespace CodeContracts.Fody.Tests
{
    public class InlineAutoFixtureAttribute : InlineAutoDataAttribute
    {
        public InlineAutoFixtureAttribute(params object[] values) 
            : base(new AutoFixtureAttribute(), values)
        {

        }
    }
}
