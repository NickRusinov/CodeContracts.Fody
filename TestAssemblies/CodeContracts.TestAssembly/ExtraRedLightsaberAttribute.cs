using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeContracts.TestAssembly
{
    public class ExtraRedLightsaberAttribute : RedLightsaberAttribute
    {
        public ExtraRedLightsaberAttribute(params object[] args)
            : base(args)
        {
            
        }
    }
}
